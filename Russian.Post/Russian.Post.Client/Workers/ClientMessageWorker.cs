using Russian.Post.Business.Logic.Services.ClientMessages;
using Russian.Post.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
                    Console.ForegroundColor = ConsoleColor.Red;

                    var messages = _client.AllDelivered().Result;
                    foreach (var result in messages.Result)
                        Console.WriteLine(result.Message);

                    Console.ResetColor();
                }
                else
                {
                    var result = _client.SendNewMessage(new AddMessageForm { Message = message }).Result;
                    if (!result.IsCorrect)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"A new message has not been sent. Error: {result.Error.Message}");
                        Console.ResetColor();
                    }
                }
            }
        }
    }
}
