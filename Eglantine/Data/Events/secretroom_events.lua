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