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

void WINAPI discord_update_presence(const char* state, const char* details) {
	DiscordRichPresence presence = { 0 };
	presence.state = state;
	presence.details = details;
	Discord_UpdatePresence(&presence);
}

void WINAPI discord_free() {
	Discord_ClearPresence();
	Discord_Shutdown();
}