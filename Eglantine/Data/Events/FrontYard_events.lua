frontYardFlowerPicked = false

function checkForBloom()
	if Event:PlayerHasItem("Photograph") and not frontYardFlowerPicked then
		Event:EnableInteractable("Eglantine")
	end
end

function pickEglantine()
	if(Event:UsingItem("Scissors")) then
		pickup("Eglantine")
		frontYardFlowerPicked = true
	end
end