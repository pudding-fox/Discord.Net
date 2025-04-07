#include "discord_game_sdk.h"
#include "discord_game_sdk.h"

typedef struct DISCORD {
	IDiscordCore* core;
} DISCORD;

DISCORD* discord_create(DiscordClientId client_id, EDiscordCreateFlags flags);