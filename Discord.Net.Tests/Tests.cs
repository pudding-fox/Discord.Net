using NUnit.Framework;
using System;

namespace Discord.Net.Tests
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
        public void Test001()
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
    }
}
