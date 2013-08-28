static_index = 1

function lookAtPainting()
	if GameState.PaintingOpened then
		Event:ShowMessage("What a waste of a painting...")
	else
		Event:ShowMessage("What a magnificent creature!")
	end
end

function interactWithPainting()
	if Event:UsingItem("Scissors") then
		runCoroutine(function()
			Event:MovePlayerTo("Painting")
			waitUntil("Player stopped")
			--Here, the player does some cool cutting of the painting
			Event:PlaySound("Extend")
			GameState.PaintingOpened = true
			Event:EnableInteractable("Tear")
			Event:EnableInteractable("PaintingDoor")
			Event:DisableInteractable("Painting")
		end)
	end
end

function interactWithTV()
	runCoroutine(function()
		if not GameState.TVOn then
			Event:MovePlayerTo("TV")
			waitUntil("Player stopped")
			--Clicking sound effect
			waitSeconds(1)
			Event:ShowMessage("No good.  It won't turn on.")
		else
			Event:MovePlayerTo("TV")
			waitUntil("Player stopped")
			--Clicking sound effect
			waitSeconds(1)
			Event:ShowMessage("It won't turn off.  The dials don't do anything.")
		end
	end)
end


function lookAtTV()
	if not GameState.TVOn then
		Event:ShowMessage("That TV looks pretty old.")
	else
		Event:OpenTV()
	end
end


function turnOnTV()
	if Event:PlayerHasItem("Photograph") then
		GameState.TVOn = true
		checkTV()
		Event:DisableTrigger("TVActivate")
	end
end


function checkTV()
	if GameState.TVOn then
		setTVImage()
		static_index = 1
		runCoroutine(staticLoop)
		Event:PlaySoundLooping("static", .5, 0, 0)
		--Begin playing static sound, but find a way to turn it off on leave
	end
end

function staticLoop()
	while Event:PlayerInRoom("LivingRoom") do
	
		if Event:PlayerInRoom("LivingRoom") then
			Event:EnableInteractable("Static"..static_index)
		end
		
		waitSeconds(.05)
		
		if Event:PlayerInRoom("LivingRoom") then
			Event:DisableInteractable("Static"..static_index)
		end
		
		static_index = static_index+1
		if static_index > 6 then
			static_index = 1
		end
	end
end



function setTVImage()
	if GameState.KitchenWindowBroken and not GameState.KitchenFlowerPicked then
		Event:SetTVImage("Graphics/TV/tv_kitchen")
		return
	else
		Event:SetTVImage("Graphics/TV/tv_eglantine")
	end	
end

function leaveLivingRoom()
	Event:StopSoundEffect("static")
end