function interactWithSafe()
	runCoroutine(function()
		
		Event:MovePlayerTo("Safe")
		waitUntil("Player stopped")
		
		--Bring up the safe screen here
		Event:OpenSafe()
		waitUntil("Safe closed")
		if Event:PlayerHasItem("Key") and not GameState.KitchenWindowBroken then
			Event:DisableSaving()
			waitSeconds(3)
			breakGlassInKitchen()
			Event:EnableSaving()
		end
	end)	
end

function breakGlassInKitchen()
	Event:PlaySound("windowbreak")
	GameState.KitchenWindowBroken = true
end