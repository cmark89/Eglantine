function openCabinet()
	runCoroutine(function()
		Event:MovePlayerTo("CabinetClosed")
		waitUntil("Player stopped")
		--Open the cabinet now
		Event:PlaySound("cabinetopen")
		Event:EnableInteractable("CabinetOpen")
		Event:EnableInteractable("DollHead")
		Event:DisableInteractable("CabinetClosed")
		
	end)
end


function closeCabinet()
	runCoroutine(function()
		Event:MovePlayerTo("CabinetOpen")
		waitUntil("Player stopped")
		--Close the cabinet		
		Event:PlaySound("cabinetclose")
		Event:EnableInteractable("CabinetClosed")
		Event:DisableInteractable("CabinetOpen")
		Event:DisableInteractable("DollHead")
	end)
end

function interactWithDollHead()
	Event:ShowMessage("Yeah, there's no way I'm touching that.  It's obviously cursed.")
end