function pickUpPuzzlebox()
	runCoroutine(function()
		Event:MovePlayerTo("Puzzlebox")
		waitUntil("Player stopped")
		Event:GainItem("Puzzlebox")
		Event:DisableInteractable("Puzzlebox")
		Event:PlaySound("Extend")
	end)
end

function lookAtPuzzlebox()
	Event:ShowMessage("A puzzlebox sits on the table.")
end

function interactWithTrapdoor()
	runCoroutine(function()
		if Event:UsingItem("Key") then
			Event:MovePlayerTo("TrapdoorClosed")
			waitUntil("Player stopped")
			Event:PlaySound("Extend")
			Event:DestroyItem("Key")
			Event:DisableInteractable("TrapdoorClosed")
			Event:EnableInteractable("TrapdoorOpen")
			Event:EnableInteractable("TrapdoorOpenGraphic")
		else
			Event:MovePlayerTo("TrapdoorClosed")
			waitUntil("Player stopped")
			--Play rattling sound
			Event:ShowMessage("It's locked up nice and tight.")
		end
	end)
end