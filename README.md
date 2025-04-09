#A really simple .net wrapper for discord-rpc.dll

See the tests.

```c#
DiscordManager.Create(CLIENT_ID);
var state = this.GetState();
var details = this.GetDetails();
DiscordManager.UpdatePresence(state, details);

var timer = new Timer();
timer.Elapsed += (sender, e) => DiscordManager.RunCallbacks();

...

DiscordManager.Free();
```
