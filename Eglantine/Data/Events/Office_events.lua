function openTopDrawer()
	runCoroutine(function()
		--Check if the player is trying to use the crowbar
		usingCrowbar = false
		if (Event:UsingItem("Crowbar")) then 
			usingCrowbar = true 
		end
	
		--Move the player to the filing cabinet
		Event:MovePlayerTo("TopDrawer_Closed")
		waitUntil("Player stopped")
		
		--Now, check to see what must happen.
		if usingCrowbar and not GameState.TopDrawerFixed then
			--Animation as player pries it open...
			GameState.TopDrawerFixed = true
			Event:PlaySound("safeopen")
			setTopDrawerOpen()
		elseif GameState.TopDrawerFixed then
			setTopDrawerOpen()
		else
			Event:ShowMessage("It's stuck shut.  I don't have the strength to open it.")
		end
	end)
end


function openBottomDrawer()
	runCoroutine(function()
		--Check if the player is trying to use the crowbar
		usingCrowbar = false
		if (Event:UsingItem("Crowbar")) then 
			usingCrowbar = true 
		end
	
		--Move the player to the filing cabinet
		Event:MovePlayerTo("BottomDrawer_Closed")
		waitUntil("Player stopped")
		
		--Now, check to see what must happen.
		if usingCrowbar and not GameState.BottomDrawerFixed then
			--Animation as player pries it open...
			GameState.BottomDrawerFixed = true
			Event:PlaySound("safeopen")
			setBottomDrawerOpen()
		elseif GameState.BottomDrawerFixed then
			setBottomDrawerOpen()
		else
			Event:ShowMessage("It's completely jammed.  I wonder how long it's been since it's been opened...")
		end
	end)
end


function closeTopDrawer()
	runCoroutine(function()
		Event:MovePlayerTo("TopDrawer_Open")
		waitUntil("Player stopped")
		setTopDrawerClosed()
	end)
end

function closeBottomDrawer()
	runCoroutine(function()
		Event:MovePlayerTo("BottomDrawer_Open")
		waitUntil("Player stopped")
		setBottomDrawerClosed()
	end)
end


function setTopDrawerOpen()
	Event:PlaySound("filingcabinetopen")
	
	Event:DisableInteractable("TopDrawer_Closed")
	Event:EnableInteractable("TopDrawer_Open")
	
	--Enable all items that haven't been taken yet...
	if not GameState:PlayerHasItem("Blueprints") then
		Event:EnableInteractable("Blueprints")
	end
	
	if not GameState:PlayerHasItem("Strange Notes") then
		Event:EnableInteractable("Strange Notes")
	end
	
	checkCabinetBlocker()
end

function setBottomDrawerOpen()
	Event:PlaySound("filingcabinetopen")
	
	Event:DisableInteractable("BottomDrawer_Closed")
	Event:EnableInteractable("BottomDrawer_Open")
	
	--Enable all items that haven't been taken yet...
	--Enable all items that haven't been taken yet...
	if not GameState:PlayerHasItem("Journal") then
		Event:EnableInteractable("Journal")
	end
	
	if not GameState.PhotoTaken then
		Event:EnableInteractable("Photograph")
	end
	
	checkCabinetBlocker()
end

function setTopDrawerClosed()
	Event:PlaySound("filingcabinetclose")
	
	Event:DisableInteractable("TopDrawer_Open")
	Event:EnableInteractable("TopDrawer_Closed")
	
	--Disable all items that haven't been taken yet...
	Event:DisableInteractable("Strange Notes")
	Event:DisableInteractable("Blueprints")
	
	checkCabinetBlocker()
end

function setBottomDrawerClosed()
	Event:PlaySound("filingcabinetclose")

	Event:DisableInteractable("BottomDrawer_Open")
	Event:EnableInteractable("BottomDrawer_Closed")
	
	--Enable all items that haven't been taken yet...
	Event:DisableInteractable("Photograph")
	Event:DisableInteractable("Journal")
	
	checkCabinetBlocker()
end

--Prevent moving into the cabinet if one of the drawers is open
function checkCabinetBlocker()
	if Event:InteractableIsActive("BottomDrawer_Open") or Event:InteractableIsActive("TopDrawer_Open") then
		if not Event:InteractableIsActive("CabinetMovementBlocker") then
			Event:EnableInteractable("CabinetMovementBlocker")
		end
	else
		Event:DisableInteractable("CabinetMovementBlocker")
	end
end