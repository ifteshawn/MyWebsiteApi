using MyWebsiteApi.Services;
using Microsoft.Azure.Cosmos;
using Resend;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOptions();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProfileDataService>(options =>
{
    string URL = builder.Configuration.GetSection("CosmosDBSettings")
    .GetValue<string>("URL")!;

    string PrimaryKey = builder.Configuration.GetSection("CosmosDBSettings")
    .GetValue<string>("PrimaryKey")!;

    string databaseName = builder.Configuration.GetSection("CosmosDBSettings")
    .GetValue<string>("DatabaseName")!;
    string containerName = builder.Configuration.GetSection("CosmosDBSettings")
    .GetValue<string>("ContainerName")!;
    var cosmosClient = new CosmosClient(URL, PrimaryKey);

    return new ProfileDataService(cosmosClient, databaseName, containerName);
});

builder.Services.AddTransient<IMailDataService, MailDataService>();

// builder.Services.Configure<MailData>(builder.Configuration.GetSection("MailData"));

builder.Services.AddHttpClient();

builder.Services.Configure<ResendClientOptions>(o =>
{
    // var configuration = builder.Configuration.GetSection("ResendSettings");
    // configuration.Bind(o.ApiToken);
    o.ApiToken = builder.Configuration.GetSection("MailSettings").GetValue<string>("ResendApiToken")!;
});

builder.Services.AddTransient<IResend, ResendClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();