using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ListMyWallofText
{
    class Program
    {
        static TelegramBotClient myBot = new TelegramBotClient("<BotAPIKeyHere");

        
        static void Main()
        {
                myBot.StartReceiving();
                myBot.OnMessage += Bot_OnMessage;
            
            Console.ReadLine();
        }

        private static void Bot_OnMessage(object test, MessageEventArgs e)
        {
            try
            {
                
               if (e.Message.Type.ToString() != "Text")
               {
                    var send_error_response = $"{e.Message.Chat.FirstName},I support only text messages";
                    myBot.SendTextMessageAsync(e.Message.Chat.Id, send_error_response) ;
               }

                else
                {
                    HandleMessageResponse(e);

                }
            }

            catch (Exception)
            {
                throw;

            }
            

        }
        /// <summary>
        /// Take the Text Message and handle it as required.
        /// </summary>
        /// <param name="e"></param>
        private static void HandleMessageResponse(MessageEventArgs e)
        {
            var messagetext = e.Message.Text;

            if (messagetext == "/start")
            {
                SendStartResponse(e);
                Console.WriteLine("Start Response Received.");
            }
            else
            {
                SendListReponse(e, messagetext);
                Console.WriteLine("Response Sent.");
            }
        }
        /// <summary>
        /// Splits the message received into a string array and sends them all back.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="messagetext"></param>
        private static void SendListReponse(MessageEventArgs e, string messagetext)
        {
            char[] splitter = new[] { '\n' };
            string[] stringlist = messagetext.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

            if (stringlist.Length >= 0)
            {
                for (var i = 0; i <= (stringlist.Length - 1); i++)
                {
                    myBot.SendTextMessageAsync(e.Message.Chat.Id, stringlist[i]);
                }
            }
        }
        /// <summary>
        /// Sends out the first response a user gets
        /// </summary>
        /// <param name="e"></param>
        private static void SendStartResponse(MessageEventArgs e)
        {
            var send_start_response = $@"Hi {e.Message.Chat.FirstName}!, I take your sentences in a message, split them into individual messages and send them back to you. 
                        Source code for this bot is hosted at https://github.com/YEGCSharpDev/ListMyWallofText";

            myBot.SendTextMessageAsync(e.Message.Chat.Id, send_start_response);
        }
    }
}
