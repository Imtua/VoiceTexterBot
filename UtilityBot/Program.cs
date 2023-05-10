using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using UtilityBot;

internal class Program
{
    public static async Task Main()
    {
        Console.OutputEncoding = Encoding.Unicode;

        var host = new HostBuilder()
            .ConfigureServices((hostContext, services) => ConfigureServices(services))
            .UseConsoleLifetime()
            .Build();

        Console.WriteLine("Бот UtilityBot запущен");
        await host.RunAsync();
        Console.WriteLine("Сервис UtilityBot остановлен");
    }

    static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient
        ("6180399368:AAHrrsRnBGTQgV3dYJ9msT7HX0VlnFRdoXU"));
        services.AddHostedService<Bot>();
    }
}