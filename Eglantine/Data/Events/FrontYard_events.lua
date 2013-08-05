function checkForBloom()
	if Event:PlayerHasItem("Photograph") and not GameState.FrontYardFlowerPicked then
		Event:EnableInteractable("Eglantine")
	end
end

function pickFrontYardEglantine()
	if(Event:UsingItem("Scissors")) then
		pickup("Eglantine")
		GameState.FrontYardFlowerPicked = true
	end
end