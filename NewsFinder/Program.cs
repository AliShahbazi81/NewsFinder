using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NewsFinder.DataAccess.Data.DbContext;
using NewsFinder.Services.ChatGPT.Services;
using NewsFinder.Services.ChatGPT.Services.ChatGPT;
using NewsFinder.Services.MessagingAPI.Services.Email;
using NewsFinder.Services.MessagingAPI.Services.SMS;
using NewsFinder.Services.MessagingAPI.Services.SMS.Settings;
using NewsFinder.Services.TelegramAPI.Services;
using NewsFinder.Services.TelegramAPI.Services.News;
using NewsFinder.Services.TelegramAPI.Services.News.GetPosts;
using NewsFinder.Services.TwittersAPI.Services;
using NewsFinder.Services.TwittersAPI.Services.News;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//! -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ Register services -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_!

//* Database
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//* Twilio
builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("TwilioCredentials"));
builder.Services.AddScoped(x => x.GetRequiredService<IOptions<TwilioSettings>>().Value);
builder.Services.AddScoped<ITwilioService, TwilioService>();

//* Email Settings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailCredentials"));

//* Telegram bot client
var telegramBotSettings = builder.Configuration.GetSection("TelegramCredentials");

builder.Services.Configure<TelegramSendingOptions>(telegramBotSettings);
builder.Services.AddScoped<IPostInNews, PostInNews>();
builder.Services.AddScoped<IGetNews, GetNews>();
builder.Services.AddScoped<ITelegramBotClient>(x =>
{
    var settings = x.GetRequiredService<IOptions<TelegramSendingOptions>>().Value;
    return new TelegramBotClient(settings.API_TOKEN);
});

//* ChatGPT API
builder.Services.Configure<ChatGptSettings>
    (builder.Configuration.GetSection("ChatGPTCredentials"));
builder.Services.AddHttpClient<IChatGptService, ChatGptService>();

//* Twitter API
builder.Services.Configure<TwitterSettings>
    (builder.Configuration.GetSection("TwitterCredentials"));
builder.Services.AddScoped<ITwitterNews, TwitterNews>();


//! -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ End of Registering services -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_!

builder.Services.AddOptions();
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