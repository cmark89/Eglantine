--setup.lua--
--This script is used for exposing certain C# classes and instances to the other .lua files.
--Currently exposed: GameState, EventManager
--It also loads all other .lua files required by the game to streamline the process of loading

--Load the other .lua files required by the game
require "Data/testevents"
require "Data/rooms"
require "Data/items"
require "Data/scheduler"

--Load the relevent assemblies and types
luanet.load_assembly "Eglantine"
GAME_STATE = luanet.import_type "Eglantine.Engine.GameState"
EVENT_MANAGER = luanet.import_type "Eglantine.Engine.EventManager"

--Initialize globals to nil
GameState = nil
Event = nil

--Functions related to .NET classes and objects
--Sets the lua global GameState to the current GameState.Instance--
function loadGameState()
	print("GameState loaded.")
	GameState = GAME_STATE.Instance
end

--Sets the lua global Event to the current EventManager.Instance--
function loadEventManager()
	print("EventManager loaded.")
	Event = EVENT_MANAGER.Instance
end

--Event:ShowMessage(string)
--Event:MovePlayer(targetX, targetY)
--Event:ChangeRoom(roomName, roomEntranceName)