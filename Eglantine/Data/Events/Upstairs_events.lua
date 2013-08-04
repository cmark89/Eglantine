function interactWithSafe()
	runCoroutine(function()
		
		Event:MovePlayerTo("Safe")
		waitUntil("Player stopped")
		
		--Bring up the safe screen here
		Event:OpenSafe()
		waitUntil("Safe closed")
		waitSeconds(3)
		if Event:PlayerHasItem("Key") and not GameState.KitchenWindowBroken then
			breakGlassInKitchen()
		end
	end)	
end

function breakGlassInKitchen()
	Event:PlaySound("windowbreak")
	GameState.KitchenWindowBroken = true
end