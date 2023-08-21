using Microsoft.Extensions.Options;
using NewsFinder.Services.ChatGPT.Services;
using NewsFinder.Services.MessagingAPI.Services.Email;
using NewsFinder.Services.MessagingAPI.Services.SMS;
using NewsFinder.Services.MessagingAPI.Services.SMS.Settings;
using NewsFinder.Services.TelegramAPI.Services;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//! -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ Register services -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_!

//* Twilio
builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("TwilioCredentials"));
builder.Services.AddSingleton(x => x.GetRequiredService<IOptions<TwilioSettings>>().Value);
builder.Services.AddScoped<ITwilioService, TwilioService>();

//* Email Settings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailCredentials"));

//* Telegram bot client
var telegramBotSettings = builder.Configuration.GetSection("TelegramCredentials");

builder.Services.Configure<TelegramSendingOptions>(telegramBotSettings);
builder.Services.AddSingleton<ITelegramBotClient>(x =>
{
    var settings = x.GetRequiredService<IOptions<TelegramSendingOptions>>().Value;
    return new TelegramBotClient(settings.API_TOKEN);
});

//* ChatGPT API
builder.Services.Configure<ChatGptSettings>
    (builder.Configuration.GetSection("ChatGPTCredentials"));


//! -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ End of Registering services -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_!


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();