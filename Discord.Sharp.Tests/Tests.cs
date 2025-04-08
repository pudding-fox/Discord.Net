using NUnit.Framework;
using System;
using System.Security.Principal;

namespace Discord.Sharp.Tests
{
    [TestFixture]
    public class Tests
    {
        public const long CLIENT_ID = 1357689312660946984;

        [SetUp]
        public void SetUp()
        {
            DiscordManager.Load();
        }

        [TearDown]
        public void TearDown()
        {
            DiscordManager.Unload();
        }

        [Test]
        public void Create()
        {
            var discord = DiscordManager.Create(CLIENT_ID, DiscordManager.CreateFlags.NoRequireDiscord);
            if (IntPtr.Zero.Equals(discord))
            {
                Assert.Fail();
            }
            try
            {
                var result = DiscordManager.GetResult(discord);
                Assert.AreEqual(DiscordManager.Result.Ok, result);
            }
            finally
            {
                DiscordManager.Free(discord);
            }
        }

        [Test]
        public void Authenticate()
        {
            var discord = DiscordManager.Create(CLIENT_ID, DiscordManager.CreateFlags.NoRequireDiscord);
            if (IntPtr.Zero.Equals(discord))
            {
                Assert.Fail();
            }
            try
            {
                {
                    var result = DiscordManager.GetResult(discord);
                    Assert.AreEqual(DiscordManager.Result.Ok, result);
                }
                {
                    DiscordManager.FetchToken(discord);
                    var result = DiscordManager.WaitForResult(discord);
                    Assert.AreEqual(DiscordManager.Result.Ok, result);
                }
                {
                    var token = default(DiscordManager.OAuth2Token);
                    DiscordManager.GetToken(discord, ref token);
                    Assert.IsNotEmpty(token.AccessToken);
                    Assert.IsNotEmpty(token.Scopes);
                    Assert.Greater(token.Expires, 0);
                }
            }
            finally
            {
                DiscordManager.Free(discord);
            }
        }

        [Test]
        public void UpdateAndClearActivity()
        {
            var discord = DiscordManager.Create(CLIENT_ID, DiscordManager.CreateFlags.NoRequireDiscord);
            if (IntPtr.Zero.Equals(discord))
            {
                Assert.Fail();
            }
            try
            {
                {
                    var result = DiscordManager.GetResult(discord);
                    Assert.AreEqual(DiscordManager.Result.Ok, result);
                }
                {
                    DiscordManager.FetchToken(discord);
                    var result = DiscordManager.WaitForResult(discord);
                    Assert.AreEqual(DiscordManager.Result.Ok, result);
                }
                {
                    var token = default(DiscordManager.OAuth2Token);
                    DiscordManager.GetToken(discord, ref token);
                    Assert.IsNotEmpty(token.AccessToken);
                    Assert.IsNotEmpty(token.Scopes);
                    Assert.Greater(token.Expires, 0);
                }
                {
                    var activity = default(DiscordManager.Activity);
                    activity.Type = DiscordManager.ActivityType.Playing;
                    activity.State = Guid.NewGuid().ToString();
                    activity.Details = Guid.NewGuid().ToString();
                    DiscordManager.SetActivity(discord, ref activity);
                }
                {
                    var result = DiscordManager.WaitForResult(discord);
                    Assert.AreEqual(DiscordManager.Result.Ok, result);
                }
                {
                    DiscordManager.ClearActivity(discord);
                    var result = DiscordManager.WaitForResult(discord);
                    Assert.AreEqual(DiscordManager.Result.Ok, result);
                }
            }
            finally
            {
                DiscordManager.Free(discord);
            }
        }
    }
}
