using Russian.Post.Business.Logic.Services.ClientMessages;
using Russian.Post.Forms;
using System;

namespace Russian.Post.Client.Workers
{
    internal sealed class ClientMessageWorker
    {
        private readonly IClientMessagesService _client;

        public ClientMessageWorker(IClientMessagesService client) => _client = client;

        public void DoWork()
        {
            ConsoleKeyInfo exit = default;
            while (exit.Key != ConsoleKey.Q)
            {
                Console.WriteLine();
                Console.Write("Input new message: ");

                var message = Console.ReadLine();
                if (string.Equals(message, "print", StringComparison.OrdinalIgnoreCase))
                {
                    var messages = _client.AllDelivered().Result;
                    if (messages.IsCorrect)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;

                        foreach (var result in messages.Result)
                            Console.WriteLine($" Message: {result.Message}  IP-Address: {result.IpAddress}  Date: {result.CreatedAt}");

                        Console.ResetColor();
                    }
                    else
                    {
                        HandleErrors($"A network-related error has occurred. Error: {messages.Error.Message}");
                    }
                }
                else
                {
                    var result = _client.SendNewMessage(new AddMessageForm { Message = message }).Result;
                    if (!result.IsCorrect)
                    {
                        HandleErrors($"A new message has not been sent. Error: {result.Error.Message}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"   Message: {message} has been successfully delivered.");
                        Console.ResetColor();
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Press eny key to continue ...");
                Console.WriteLine("Press 'q' to quit ... ");
                exit = Console.ReadKey();
            }
        }

        private static void HandleErrors(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
