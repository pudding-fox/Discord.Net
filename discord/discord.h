#include "discord_register.h"
#include "discord_rpc.h"
#include "Windows.h"

void WINAPI discord_create(const char* applicationId);

void WINAPI discord_update_presence(const char* state, const char* details, const char* smallImageText, const char* smallImageKey, const char* largeImageText, const char* largeImageKey);

void WINAPI discord_clear_presence();

void WINAPI discord_run_callbacks();

void WINAPI discord_free();