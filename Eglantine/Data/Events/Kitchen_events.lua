function checkWindow()
	if GameState.KitchenWindowBroken then
		Event:DisableInteractable("WindowLayer")
		Event:EnableInteractable("BrokenWindowLayer")
		
		if not GameState.KitchenFlowerPicked then
			Event:EnableInteractable("Eglantine")
		end
	else
		Event:EnableInteractable("WindowLayer")
		Event:DisableInteractable("BrokenWindowLayer")
		Event:DisableInteractable("Eglantine")
	end
end

function pickKitchenEglantine()
	if(Event:UsingItem("Scissors")) then
		pickup("Eglantine")
		GameState.KitchenFlowerPicked = true
	end
end