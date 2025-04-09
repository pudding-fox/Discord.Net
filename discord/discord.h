#include "discord_register.h"
#include "discord_rpc.h"
#include "Windows.h"

void WINAPI discord_create(const char* applicationId);

void WINAPI discord_run_callbacks();

void WINAPI discord_free();