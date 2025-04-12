#include "discord.h"

#if _DEBUG
#include <stdio.h>
#endif

void WINAPI discord_create(const char* applicationId) {
	DiscordEventHandlers handlers = { 0 };
	Discord_Initialize(applicationId, &handlers, 1, NULL);
}

void WINAPI discord_run_callbacks() {
	Discord_RunCallbacks();
}

void WINAPI discord_update_presence(const char* state, const char* details, const char* smallImageText, const char* smallImageKey, const char* largeImageText, const char* largeImageKey) {
	DiscordRichPresence presence = { 0 };
	presence.state = state;
	presence.details = details;
	presence.smallImageText = smallImageText;
	presence.smallImageKey = smallImageKey;
	presence.largeImageText = largeImageText;
	presence.largeImageKey = largeImageKey;
	Discord_UpdatePresence(&presence);
}

void WINAPI discord_clear_presence() {
	Discord_ClearPresence();
}

void WINAPI discord_free() {
	Discord_ClearPresence();
	Discord_Shutdown();
}