using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;
using DiscordRPC.Logging;

namespace discord_rpc
{
    internal class Program
    {
        private static DiscordRpcClient m_client;

        private static void Main(string[] args)
        {
            Initialize();

            // Keep the console application running
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            // Make sure to clean up resources when exiting
            Deinitialize();
        }

        private static void Initialize()
        {
            m_client = new DiscordRpcClient("1193944736088723476");

            //Set the logger
            m_client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            //Subscribe to events
            m_client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Ready: {0}", e.User.Username);
            };

            m_client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("Update: {0}", e.Presence);
            };

            //Connect to the RPC
            m_client.Initialize();

            //Set the rich presence
            //Call this as many times as you want and anywhere in your code.
            m_client.SetPresence(new RichPresence()
            {
                Details = "PayPal/Cryptocurrecy",
                State = "Instant Delivery",
                Assets = new Assets()
                {
                    LargeImageKey = "unixx_logo",
                    SmallImageKey = "https://i.imgur.com/AkbxrKM.gif"
                },
                Buttons = new Button[]
                {
                    new Button() { Label = "🛒 Purchase", Url = "https://unixx.shop" },
                    new Button() { Label = "🌐 Discord", Url = "https://discord.gg/knrJVBR6T9" },
                }
            });
        }

        private static void Deinitialize()
        {
            m_client.Dispose();
        }
    }
}
