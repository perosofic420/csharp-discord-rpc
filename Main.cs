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
        private static Timer m_timer;

        private static void Main(string[] args)
        {
            Initialize();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            Deinitialize();
        }

        private static void Initialize()
        {
            m_client = new DiscordRpcClient("changeme");

            m_client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            m_client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Ready: {0}", e.User.Username);
            };

            m_client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("Update: {0}", e.Presence);
            };

            m_client.Initialize();

            SetPresence();

            m_timer = new Timer(UpdatePresence, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
        }

        private static void SetPresence()
        {
            m_client.SetPresence(new RichPresence()
            {
                Details = "changeme",
                State = "changeme",
                Assets = new Assets()
                {
                    LargeImageKey = "changeme",
                    SmallImageKey = "changeme"
                },
                Buttons = new Button[]
                {
                    new Button() { Label = "changeme", Url = "changeme" },
                    new Button() { Label = "changeme", Url = "changeme" },
                }
            });
        }

        private static void UpdatePresence(object state)
        {
            m_client.SetPresence(new RichPresence()
            {
                Details = "changeme",
                State = "changeme",
                Assets = new Assets()
                {
                    LargeImageKey = "changeme",
                    SmallImageKey = "changeme"
                },
                Buttons = new Button[]
                {
                    new Button() { Label = "changeme", Url = "changeme" },
                    new Button() { Label = "changeme", Url = "changeme" },
                }
            });
        }

        private static void Deinitialize()
        {
            m_client.Dispose();
        }
    }
}
