enum ClientToServerId
{
    sendName = 0,
    sendMoveInputs = 1,
    sendChatMessage = 2,

    LOBBY_requestSetReady = 50,
    LOBBY_LOAD_PLAYERS = 51,
    LOBBY_GAME_SCENE_LOADED = 52,
    
    GAME_REQUESTHIT = 100,
    GAME_REQUEST_USE_ABILITY = 101,
    GAME_REQUEST_BUY_ABILITY = 102
}


enum ServerToClientId
{
    spawnPlayer = 0,
    playerDisconnected = 1,
    playerMovement = 2,
    playerChatMessage = 3,

    setAllPlayerPositions = 10,
    playerTeleport = 11,
    SET_UI_TIMER = 12,
    REMOVE_UI_TIMER = 13,


    LOBBY_responseSetReady = 50,
    LOBBY_SWITCH_TO_GAME_SCENE = 51,
    GAME_STARTED = 52,

    GAME_NEWROUND = 100,
    GAME_RESPONSE_HIT = 101,
    GAME_RESPONSE_USE_ABILITY = 102,
    GAME_RESPONSE_BUY_ABILITY = 103,
    GAME_HURT_PLAYER = 104,
    GAME_PLAYER_DEAD = 105,
}
