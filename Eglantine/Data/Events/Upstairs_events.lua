function interactWithSafe()
	runCoroutine(function()
		
		Event:MovePlayerTo("Safe")
		waitUntil("Player stopped")
		
		--Bring up the safe screen here
		Event:OpenSafe()
	end)	
end