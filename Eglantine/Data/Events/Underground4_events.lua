--Check the state of the game and determine which ending scenario should occur
function beginEnding()
	Event:DisableSaving()
	Event:SetItemType("Eglantine", "Active")
	--Right now, only the default is available, so enable it
	GameState.Ending = "normal"
	Event:EnableInteractable("Grave")
	Event:DisableInteractable("Artifact")
	runCoroutine(normalEndingScenario)
end

--This is the normal ending scenario
function normalEndingScenario()
	--Loop the pulsing
	strobeTime = 6
	while strobeTime > 0 and GameState.FlowersOnGrave < 3 do
		waitSeconds(strobeTime)
		artifactPulse(.4)
		strobeTime = strobeTime -1
	end
	
	--If the player failed to complete the puzzle
	if GameState.FlowersOnGrave < 3 then
		waitSeconds(1)
		Event:FadeOutInteractable("Grave", 3)
		Event:EnableInteractable("Artifact")
		Event:FadeInInteractable("Artifact", 3)
		waitSeconds(3)
		Event:DisableInteractable("Grave")
		waitSeconds(4)
		Event:PlayStorySequence("badEnding");
	end
	
	--If the player honored the dead
	if GameState.FlowersOnGrave == 3 then
		waitSeconds(4)
		fadeOutGrave(6)
		waitSeconds(6)
		Event:PlayStorySequence("goodEnding");
	end
end


--Pulses the grave and artifact in and out over the given time (each way)
function artifactPulse(time)
	Event:PlaySound("heartbeat")
	fadeOutGrave(time)
	Event:EnableInteractable("Artifact")
	Event:FadeInInteractable("Artifact", time)
	waitSeconds(.5)
	Event:FadeOutInteractable("Artifact", time)
	fadeInGrave(time)
	waitSeconds(.5)
	Event:DisableInteractable("Artifact")
end

function interactWithGrave()
	if(Event:UsingItem("Eglantine")) then
		runCoroutine(function()
			Event:MovePlayerTo("Grave")
			waitUntil("Player stopped")
			placeFlowerOnGrave()
		end)
	end
end

function placeFlowerOnGrave()
	Event:DestroyItem("Eglantine")
	GameState.FlowersOnGrave = GameState.FlowersOnGrave + 1
	Event:EnableInteractable("Flower"..GameState.FlowersOnGrave)
end


--Wrapper functions to fade in and out the grave and all attached flowers
function fadeOutGrave(time)
	Event:FadeOutInteractable("Grave", time)
	Event:FadeOutInteractable("Flower1", time)
	Event:FadeOutInteractable("Flower2", time)
	Event:FadeOutInteractable("Flower3", time)
end

function fadeInGrave(time)
	Event:FadeInInteractable("Grave", time)
	Event:FadeInInteractable("Flower1", time)
	Event:FadeInInteractable("Flower2", time)
	Event:FadeInInteractable("Flower3", time)
end


function readHeadstone()
	if GameState.FlowersOnGrave == 0 then
		Event:ShowMessage("A gravestone... the name has been worn away.")
	end
	
	if GameState.FlowersOnGrave == 1 then
		Event:ShowMessage("The writing is very faded, but its partially legible.  It reads: ..R. .I.S FC.A-II.C W.AIV.R.")
	end
	
	if GameState.FlowersOnGrave == 2 then
		Event:ShowMessage("The text is becoming clearer.  It reads: \nHER. LICS FCLAHIIVE WCATNERS")
	end
	
	if GameState.FlowersOnGrave == 3 then
		Event:ShowMessage("The headstone reads: \nHERE LIES EGLANTINE WEATHERS")
	end
end