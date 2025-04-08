using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Discord.Net
{
    public class DiscordManager
    {
        const string DllName = "discord";

        public static void Load()
        {
            Loader.Load("discord_game_sdk");
            Loader.Load(DllName);
        }

        public static void Unload()
        {
            Loader.Free(DllName);
            Loader.Free("discord_game_sdk");
        }

        [DllImport(DllName)]
        static extern IntPtr discord_create(long client_id, CreateFlags flags);

        public static IntPtr Create(long clientId, CreateFlags flags)
        {
            return discord_create(clientId, flags);
        }

        [DllImport(DllName)]
        static extern Result discord_get_result(IntPtr discord);

        public static Result GetResult(IntPtr discord)
        {
            return discord_get_result(discord);
        }

        public static Result WaitForResult(IntPtr discord, int timeout = 10000, int interval = 100)
        {
            for (var attempt = 0; attempt <= timeout; attempt += interval)
            {
                RunCallbacks(discord);
                var result = GetResult(discord);
                if (result != Result.Pending)
                {
                    return result;
                }
                Thread.Sleep(interval);
            }
            throw new TimeoutException("Timed out waiting for a result.");
        }

        [DllImport(DllName)]
        static extern void discord_fetch_token(IntPtr discord);

        public static void FetchToken(IntPtr discord)
        {
            discord_fetch_token(discord);
        }

        [DllImport(DllName)]
        static extern void discord_get_token(IntPtr discord, ref OAuth2Token token);

        public static void GetToken(IntPtr discord, ref OAuth2Token token)
        {
            discord_get_token(discord, ref token);
        }

        [DllImport(DllName)]
        static extern void discord_set_activity(IntPtr discord, ref Activity activity);

        public static void SetActivity(IntPtr discord, ref Activity activity)
        {
            discord_set_activity(discord, ref activity);
        }

        [DllImport(DllName)]
        static extern void discord_update_activity(IntPtr discord);

        public static void UpdateActivity(IntPtr discord)
        {
            discord_update_activity(discord);
        }

        [DllImport(DllName)]
        static extern void discord_clear_activity(IntPtr discord);

        public static void ClearActivity(IntPtr discord)
        {
            discord_clear_activity(discord);
        }

        [DllImport(DllName)]
        static extern void discord_run_callbacks(IntPtr discord);

        public static void RunCallbacks(IntPtr discord)
        {
            discord_run_callbacks(discord);
        }

        [DllImport(DllName)]
        static extern void discord_free(IntPtr discord);

        public static void Free(IntPtr discord)
        {
            discord_free(discord);
        }

        public enum Result
        {
            Ok = 0,
            ServiceUnavailable = 1,
            InvalidVersion = 2,
            LockFailed = 3,
            InternalError = 4,
            InvalidPayload = 5,
            InvalidCommand = 6,
            InvalidPermissions = 7,
            NotFetched = 8,
            NotFound = 9,
            Conflict = 10,
            InvalidSecret = 11,
            InvalidJoinSecret = 12,
            NoEligibleActivity = 13,
            InvalidInvite = 14,
            NotAuthenticated = 15,
            InvalidAccessToken = 16,
            ApplicationMismatch = 17,
            InvalidDataUrl = 18,
            InvalidBase64 = 19,
            NotFiltered = 20,
            LobbyFull = 21,
            InvalidLobbySecret = 22,
            InvalidFilename = 23,
            InvalidFileSize = 24,
            InvalidEntitlement = 25,
            NotInstalled = 26,
            NotRunning = 27,
            InsufficientBuffer = 28,
            PurchaseCanceled = 29,
            InvalidGuild = 30,
            InvalidEvent = 31,
            InvalidChannel = 32,
            InvalidOrigin = 33,
            RateLimited = 34,
            OAuth2Error = 35,
            SelectChannelTimeout = 36,
            GetGuildTimeout = 37,
            SelectVoiceForceRequired = 38,
            CaptureShortcutAlreadyListening = 39,
            UnauthorizedForAchievement = 40,
            InvalidGiftCode = 41,
            PurchaseError = 42,
            TransactionAborted = 43,
            DrawingInitFailed = 44,
            Pending = 45
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
