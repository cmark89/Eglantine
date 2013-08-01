ending = ""
flowersOnGrave = 0

--Check the state of the game and determine which ending scenario should occur
function beginEnding()
	Event:SetItemType("Eglantine", "Active")
	--Right now, only the default is available, so enable it
	ending = "normal"
	Event:EnableInteractable("Grave")
	Event:DisableInteractable("Artifact")
	runCoroutine(normalEndingScenario)
end

--This is the normal ending scenario
function normalEndingScenario()
	waitSeconds(6)
	artifactPulse(.4)
	waitSeconds(5)
	artifactPulse(.4)
	waitSeconds(4)
	artifactPulse(.4)
	waitSeconds(3)
	artifactPulse(.4)
	waitSeconds(2)
	artifactPulse(.4)
	waitSeconds(1)
	artifactPulse(.4)
	waitSeconds(1)
	artifactPulse(.4)
	waitSeconds(1)
	artifactPulse(.4)
	waitSeconds(1)
	artifactPulse(.4)
	waitSeconds(1)
	Event:FadeOutInteractable("Grave", 4)
	Event:EnableInteractable("Artifact")
	Event:FadeInInteractable("Artifact", 4)
	waitSeconds(4)
	Event:DisableInteractable("Grave")
	waitSeconds(3)
	Event:ShowMessage("GAME OVER!")
end


--Pulses the grave and artifact in and out over the given time (each way)
function artifactPulse(time)
	Event:FadeOutInteractable("Grave", time)
	Event:EnableInteractable("Artifact")
	Event:FadeInInteractable("Artifact", time)
	waitSeconds(.5)
	Event:FadeOutInteractable("Artifact", time)
	Event:FadeInInteractable("Grave", time)
	waitSeconds(.5)
	Event:DisableInteractable("Artifact")
end

function interactWithGrave()
	if(Event:UsingItem("Eglantine")) then
		Event:MovePlayerTo("Grave")
		waitUntil("Player stopped")
		placeFlowerOnGrave()
	end
end

function placeFlowerOnGrave()
	Event:DestroyItem("Eglantine")
	flowersOnGrave = flowersOnGrave + 1
	Event:EnableInteractable("Flower"..flowersOnGrave)
end