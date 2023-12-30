using MyWebsiteApi.Services;
using Microsoft.Azure.Cosmos;
using Resend;
using MyWebsiteApi.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOptions();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProfileDataService>(options =>
{
    string cosmosConnection = builder.Configuration["ConnectionStrings:Ifte:CosmosDbConnectionString"]!;
    string databaseName = builder.Configuration["CosmosDbSettings:DatabaseName"]!;
    string containerName = builder.Configuration["CosmosDbSettings:ContainerName"]!;
    var cosmosClient = new CosmosClient(cosmosConnection);
    return new ProfileDataService(cosmosClient, databaseName, containerName);
});

builder.Services.AddTransient<IMailDataService, MailDataService>();
builder.Services.AddHttpClient<ResendClient>();
builder.Services.Configure<ResendClientOptions>(o =>
{
    o.ApiToken = builder.Configuration["Resend:ResendApiToken"]!;
});
builder.Services.AddTransient<IResend, ResendClient>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyAuthMiddlware>();

app.UseAuthorization();

app.MapControllers();

app.Run();