
function interactWithTrapdoor()
	runCoroutine(function()
		if Event:UsingItem("Key") or GameState.TrapdoorUnlocked then
			openTrapdoor()
		else
			Event:MovePlayerTo("TrapdoorClosed")
			waitUntil("Player stopped")
			--Play rattling sound
			Event:ShowMessage("It's locked up nice and tight.")
		end
	end)
end


function openTrapdoor()
	Event:MovePlayerTo("TrapdoorClosed")
	waitUntil("Player stopped")
	
	if not GameState.TrapdoorUnlocked then
		Event:DestroyItem("Key")
		GameState.TrapdoorUnlocked = true
	end
	
	Event:PlaySound("dooropen")
	Event:DisableInteractable("TrapdoorClosed")
	Event:EnableInteractable("TrapdoorOpen")
	Event:EnableInteractable("TrapdoorOpenGraphic")
	Event:EnableInteractable("TrapdoorHatch_Open")
end


function closeTrapdoor()
	runCoroutine(function()
		Event:MovePlayerTo("TrapdoorClosed")
		waitUntil("Player stopped")
		
		Event:PlaySound("safeclose")
		Event:EnableInteractable("TrapdoorClosed")
		Event:DisableInteractable("TrapdoorOpen")
		Event:DisableInteractable("TrapdoorOpenGraphic")
		Event:DisableInteractable("TrapdoorHatch_Open")
	end)
end


function checkTVStatic()
	if GameState.TVOn then
		Event:PlaySoundLooping("static", .15, 0, 0)
	end
end

function leaveSecretRoom()
	Event:StopSoundEffect("static")
end