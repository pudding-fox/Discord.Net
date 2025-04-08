using System;
using System.Runtime.InteropServices;

namespace Discord.Net
{
    public class DiscordManager
    {
        const string DllName = "discord";

        [DllImport(DllName)]
        static extern IntPtr discord_create(long client_id, CreateFlags flags);

        public static IntPtr Create(long clientId, CreateFlags flags)
        {
            return discord_create(clientId, flags);
        }

        [DllImport(DllName)]
        static extern void discord_free(IntPtr discord);

        public static void Free(IntPtr discord)
        {
            discord_free(discord);
        }


        public enum CreateFlags
        {
            Default = 0,
            NoRequireDiscord = 1,
        }

        public enum ActivityType
        {
            Playing,
            Streaming,
            Listening,
            Watching,
        }

        public enum ActivityPartyPrivacy
        {
            Private = 0,
            Public = 1,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Discord
        {
            public IntPtr Core;
            public OAuth2Token Token;
            public Activity Activity;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public partial struct OAuth2Token
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string AccessToken;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string Scopes;

            public Int64 Expires;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public partial struct ActivityTimestamps
        {
            public Int64 Start;

            public Int64 End;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public partial struct ActivityAssets
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string LargeImage;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string LargeText;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string SmallImage;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string SmallText;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public partial struct PartySize
        {
            public Int32 CurrentSize;

            public Int32 MaxSize;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public partial struct ActivityParty
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string Id;

            public PartySize Size;

            public ActivityPartyPrivacy Privacy;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public partial struct ActivitySecrets
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string Match;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string Join;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string Spectate;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public partial struct Activity
        {
            public ActivityType Type;

            public Int64 ApplicationId;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string Name;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string State;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string Details;

            public ActivityTimestamps Timestamps;

            public ActivityAssets Assets;

            public ActivityParty Party;

            public ActivitySecrets Secrets;

            public bool Instance;

            public UInt32 SupportedPlatforms;
        }
    }
}
