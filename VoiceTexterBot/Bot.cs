using Microsoft.Extensions.Hosting;
using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoiceTexterBot.Controllers;

namespace VoiceTexterBot
{
    internal class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramClient;

        private InlineMessageController _inlineMessageController;
        private DefaultMessageController _defaultMessageController;
        private TextMessageController _textMessageController;
        private VoiceMessageController _voiceMessageController;

        public Bot(ITelegramBotClient telegramClient,
            DefaultMessageController defaultMessageController,
            TextMessageController textMessageController,
            VoiceMessageController voiceMessageController)
        {
            _telegramClient = telegramClient;
            _defaultMessageController = defaultMessageController;
            _textMessageController = textMessageController;
            _voiceMessageController = voiceMessageController;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                new ReceiverOptions() { AllowedUpdates = { } },
                cancellationToken: stoppingToken);

            Console.WriteLine("Бот запущен");
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _inlineMessageController.Handle(update.CallbackQuery, ct:cancellationToken);
                return;
            }

            if (update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Voice:
                        await _voiceMessageController.Handle(update.Message, ct:cancellationToken);
                        return;
                    case MessageType.Text:
                        await _textMessageController.Handle(update.Message, ct:cancellationToken);
                        return;
                    default:
                        await _defaultMessageController.Handle(update.Message, ct:cancellationToken);
                        return;
                }
            }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {

                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n" +
                $"{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);

            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }

    }

}
