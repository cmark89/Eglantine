function enterBackYard()
	Event:StopLoopingSoundEffects()
	Event:PlaySoundLooping("naturalwind", .3, 0, 0)
end

function pickBackYardEglantine()
	if(Event:UsingItem("Scissors")) then
		pickup("Eglantine")
	end
end

function leaveBackYard()
	Event:StopLoopingSoundEffects()
	Event:PlaySoundLooping("lowwind", .25, 0, 0)
end