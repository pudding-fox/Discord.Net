#include "discord.h"

#if _DEBUG
#include <stdio.h>
#endif


static DISCORD* discord_alloc() {
	DISCORD* discord = (DISCORD*)calloc(1, sizeof(DISCORD*));
	if (!discord) {
		return NULL;
	}
	discord->token = calloc(1, sizeof(DiscordOAuth2Token));
	if (!discord->token) {
		discord_free(discord);
		return NULL;
	}
	discord->activity = calloc(1, sizeof(DiscordActivity));
	if (!discord->activity) {
		discord_free(discord);
		return NULL;
	}
	return discord;
}

void DISCORD_API log_hook(void* data, enum EDiscordLogLevel level, const char* message) {
#if _DEBUG
	printf(message);
#endif
}

DISCORD* WINAPI discord_create(DiscordClientId client_id, EDiscordCreateFlags flags) {
	DISCORD* discord = discord_alloc();
	if (!discord) {
		return NULL;
	}
	DiscordCreateParams params;
	DiscordCreateParamsSetDefault(&params);
	params.client_id = client_id;
	params.flags = flags;
	//params.event_data = discord;
	discord->result = DiscordCreate(DISCORD_VERSION, &params, &discord->core);
	if (discord->core) {
		discord->core->set_log_hook(discord->core, DiscordLogLevel_Debug, NULL, log_hook);
	}
	return discord;
}

EDiscordResult WINAPI discord_get_result(DISCORD* discord) {
	return discord->result;
}

void DISCORD_API get_oauth2_token_callback(void* data, EDiscordResult result, DiscordOAuth2Token* token) {
	DISCORD* discord = (DISCORD*)data;
	discord->result = result;
	if (!token) {
		return;
	}
	memcpy(discord->token, token, sizeof(DiscordOAuth2Token));
}

void WINAPI discord_fetch_token(DISCORD* discord) {
	discord->result = DiscordResult_Pending;
	IDiscordApplicationManager* manager = discord->core->get_application_manager(discord->core);
	manager->get_oauth2_token(manager, discord, get_oauth2_token_callback);
}

void WINAPI discord_get_token(DISCORD* discord, DiscordOAuth2Token* token) {
	if (!token) {
		return;
	}
	memcpy(token, discord->token, sizeof(DiscordOAuth2Token));
}

void WINAPI discord_set_activity(DISCORD* discord, DiscordActivity* activity) {
	if (!activity) {
		return;
	}
	memcpy(discord->activity, activity, sizeof(DiscordActivity));
}

void DISCORD_API update_activity_callback(void* data, enum EDiscordResult result) {
	DISCORD* discord = (DISCORD*)data;
	discord->result = result;
}

void WINAPI discord_update_activity(DISCORD* discord) {
	discord->result = DiscordResult_Pending;
	IDiscordActivityManager* manager = discord->core->get_activity_manager(discord->core);
	manager->update_activity(manager, discord->activity, discord, update_activity_callback);
}

void DISCORD_API clear_activity_callback(void* data, enum EDiscordResult result) {
	DISCORD* discord = (DISCORD*)data;
	discord->result = result;
}

void WINAPI discord_clear_activity(DISCORD* discord) {
	discord->result = DiscordResult_Pending;
	IDiscordActivityManager* manager = discord->core->get_activity_manager(discord->core);
	manager->clear_activity(manager, discord, clear_activity_callback);
}

void WINAPI discord_run_callbacks(DISCORD* discord) {
	discord->core->run_callbacks(discord->core);
}

void WINAPI discord_free(DISCORD* discord) {
	if (discord->core) {
		discord->core->destroy(discord->core);
	}
	if (discord->token) {
		free(discord->token);
	}
	if (discord->activity) {
		free(discord->activity);
	}
	free(discord);
}