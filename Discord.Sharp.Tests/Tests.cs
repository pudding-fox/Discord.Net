using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Discord.Sharp.Tests
{
    [TestFixture]
    public class Tests
    {
        public const string CLIENT_ID = "1357689312660946984";

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
            DiscordManager.Create(CLIENT_ID);
            DiscordManager.Free();
        }

        [Test]
        public void UpdatePresence()
        {
            DiscordManager.Create(CLIENT_ID);
            try
            {
                var state = Guid.NewGuid().ToString();
                var details = Guid.NewGuid().ToString();
                DiscordManager.UpdatePresence(state, details, null, null, null, null);
                DiscordManager.RunCallbacks();
            }
            finally
            {
                DiscordManager.Free();
            }
        }

        [Test]
        public void ClearPresence()
        {
            DiscordManager.Create(CLIENT_ID);
            try
            {
                DiscordManager.ClearPresence();
            }
            finally
            {
                DiscordManager.Free();
            }
        }
    }
}
