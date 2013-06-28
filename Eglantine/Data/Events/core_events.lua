--------------------
--core_events
--------------------
--This script holds common events to be called by the rest of the game,
--including picking up simple items and moving to doors

function pickup(item_name)
	runCoroutine(function()
		Event:MovePlayerTo(item_name)
		waitUntil("Player stopped")
		Event:GainItem(item_name)
		Event:DisableInteractable(item_name)
		Event:PlaySound("Extend")	
	end)
end

function door(door_name, target_room, target_entrance)
	runCoroutine(function()
		Event:MovePlayerTo(door_name)
		waitUntil("Player stopped")
		Event:ChangeRoom(target_room, target_entrance)
	end)
end