--setup.lua--
--This file is used for exposing certain C# classes and instances to the other .lua files.
--Currently exposed: GameState, EventManager

--Load the relevent assemblies and types
luanet.load_assembly "Eglantine"
GAME_STATE = luanet.import_type "Eglantine.Engine.GameState"
EVENT_MANAGER = luanet.import_type "Eglantine.Engine.EventManager"

--Initialize globals to nil
GameState = nil
Event = nil


--Sets the lua global GameState to the current GameState.Instance--
function loadGameState()
	GameState = GAME_STATE.Instance
end





--Sets the lua global Event to the current EventManager.Instance--
function loadEventManager()
	Event = EVENT_MANAGER.Instance
end

--Event:ShowMessage(string)
--Event:MovePlayer(targetX, targetY)
