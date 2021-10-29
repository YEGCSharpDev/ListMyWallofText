using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ListMyWallofText
{
    class Program
    {
        static TelegramBotClient myBot = new TelegramBotClient("2060115352:AAEYFswD_xX1e5zTUX0ByePJgXpcKTWtKsY");

        
        static void Main(string[] args)
        {
                myBot.StartReceiving();
                myBot.OnMessage += Bot_OnMessage;
            
            Console.ReadLine();
        }

        private static void Bot_OnMessage(object test, Telegram.Bot.Args.MessageEventArgs e)
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
                    var messagetext = e.Message.Text;
                    var stringholder = string.Empty;
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
            }

            catch (Exception)
            {
                throw ;

            }
            

        }
    }
}
