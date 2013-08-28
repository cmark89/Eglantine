function enterFrontYard()
	Event:StopLoopingSoundEffects()
	Event:PlaySoundLooping("naturalwind", .3, 0, 0)
	checkForBloom()
end

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

function leaveFrontYard()
	Event:StopLoopingSoundEffects()
	Event:PlaySoundLooping("lowwind", .25, 0, 0)
end