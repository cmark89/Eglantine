--Check the state of the game and determine which ending scenario should occur
kitchenFlowerPicked = false

function checkWindow()
	if GameState.KitchenWindowBroken then
		Event:DisableInteractable("WindowLayer")
		Event:EnableInteractable("BrokenWindowLayer")
		
		if not kitchenFlowerPicked then
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
		kitchenFlowerPicked = true
	end
end