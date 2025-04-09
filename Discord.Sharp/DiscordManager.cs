using System;
using System.Runtime.InteropServices;

namespace Discord.Sharp
{
    public class DiscordManager
    {
        const string DllName = "discord";

        public static void Load()
        {
            Loader.Load("discord-rpc");
            Loader.Load(DllName);
        }

        public static void Unload()
        {
            Loader.Free(DllName);
            Loader.Free("discord-rpc");
        }

        [DllImport(DllName)]
        static extern void discord_create(string applicationId);

        public static void Create(string applicationId)
        {
            discord_create(applicationId);
        }

        [DllImport(DllName)]
        static extern void discord_update_presence(string state, string details);

        public static void UpdatePresence(string state, string details)
        {
            discord_update_presence(state, details);
        }

        [DllImport(DllName)]
        static extern void discord_run_callbacks();

        public static void RunCallbacks()
        {
            discord_run_callbacks();
        }

        [DllImport(DllName)]
        static extern void discord_free();

        public static void Free()
        {
            discord_free();
        }
    }
}
