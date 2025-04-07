#include "discord.h"

static void log_hook(void* hook_data, enum EDiscordLogLevel level, const char* message) {
	//Nothing to do.
}

DISCORD* discord_create(DiscordClientId client_id, EDiscordCreateFlags flags) {
	DISCORD* discord = (DISCORD*)malloc(sizeof(DISCORD*));
	if (!discord) {
		return NULL;
	}
	DiscordCreateParams params;
	params.client_id = client_id;
	params.flags = flags;
	params.event_data = discord;
	EDiscordResult result = DiscordCreate(DISCORD_VERSION, &params, &discord->core);
	if (discord->core) {
		discord->core->set_log_hook(discord->core, DiscordLogLevel_Debug, NULL, log_hook);
	}
	if (result) {

	}
	return discord;
}

static void discord_get_token_callback(void* data, enum EDiscordResult result, struct DiscordOAuth2Token* token) {
	DISCORD* discord = (DISCORD*)data;
}

void discord_get_token(DISCORD* discord) {
	IDiscordApplicationManager* manager = discord->core->get_application_manager(discord->core);
	manager->get_oauth2_token(manager, discord, discord_get_token_callback);
}