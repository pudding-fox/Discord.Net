#include "discord_game_sdk.h"
#include "discord_game_sdk.h"

typedef struct DISCORD {
	IDiscordCore* core;
	DiscordOAuth2Token* token;
	DiscordActivity* activity;
	EDiscordResult result;
} DISCORD;

DISCORD* WINAPI discord_create(DiscordClientId client_id, EDiscordCreateFlags flags);

EDiscordResult WINAPI discord_get_result(DISCORD* discord);

void WINAPI discord_fetch_token(DISCORD* discord);

void WINAPI discord_get_token(DISCORD* discord, DiscordOAuth2Token* token);

void WINAPI discord_set_activity(DISCORD* discord, DiscordActivity* activity);

void WINAPI discord_update_activity(DISCORD* discord);

void WINAPI discord_clear_activity(DISCORD* discord);

void WINAPI discord_run_callbacks(DISCORD* discord);

void WINAPI discord_free(DISCORD* discord);