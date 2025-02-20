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
            m_client = new DiscordRpcClient("YOUR_APPLICATION_ID");

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

            m_timer = new Timer(UpdatePresence, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        private static void SetPresence()
        {
            m_client.SetPresence(new RichPresence()
            {
                // Describes what the user is currently doing (appears in the main activity section).
                Details = "Playing A Game",  

                // A secondary description that can give additional context about the user's activity.
                State = "Level 10 - Boss Fight", 

                Assets = new Assets()
                {
                    // The key of the large image (appears in the larger icon area).
                    LargeImageKey = "game_icon_large", 

                    // The key of the small image (appears next to the user's status).
                    SmallImageKey = "game_icon_small",  

                    // Optional: Descriptions for the images (appears when the user hovers over the image).
                    LargeImageText = "A Game", 
                    SmallImageText = "Boss Fight"     
                },

                // Buttons that appear below the activity description. You can provide links to your game, website, etc.
                Buttons = new Button[]
                {
                    new Button()
                    {
                        Label = "Play Game",  // The label text that appears on the button.
                        Url = "https://example.com/play"  // The URL the button will link to when clicked.
                    },

                    new Button()
                    {
                        Label = "Visit Website",  // Another button, with a different action.
                        Url = "https://example.com"  // The URL for the second button.
                    }
                }
            });
        }

        private static void UpdatePresence(object state)
        {
            SetPresence();
        }

        private static void Deinitialize()
        {
            m_client.Dispose();
        }
    }
}
