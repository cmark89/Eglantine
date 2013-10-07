function lookAtWindow()
	if not GameState:PlayerHasItem("Screwdriver") then
		Event:ShowMessage("I can see my house from here!")
		Event:ShowMessage("Just kidding.  That's what you call a joke.")
		Event:ShowMessage("Hey, a screwdriver!")
		Event:GainItem("Screwdriver")
		sendSignal("Get Screwdriver")
	else
		Event:ShowMessage("Now why was there a screwdriver there...?")
	end
end

lookingatoutlet = false
function lookAtOutlet()
	if(lookingatoutlet) then
		return nil
	end
		
	lookingatoutlet = true
	
	runCoroutine(function()
		Event:ShowMessage("What a shitty place to live...")
		waitUntil("Screwdriver found")
		Event:ShowMessage("I bet I could tinker with that outlet somethin' fierce!")
	end)
	
end

outlet_fixed = false

function tinkerWithOutlet()
	if(not outlet_fixed and Event:UsingItem("Screwdriver")) then
		runCoroutine(function()
			Event:MovePlayerTo("Outlet")
			waitUntil("Player stopped")
			Event:ShowMessage("I be tinkerin', yessir!")
			waitUntil("Message closed")
			Event:PlaySound("Extend")
			Event:DestroyItem("Screwdriver")
			outlet_fixed = true
			Event:EnableTrigger("CreakyBoard")
		end)
	end
end


function creakyBoard()
	Event:ChangeRoom("secretroom", "Door")
end
	
	-- if gameTime < nextCreakTime then do
		-- return
	-- else do
		-- Get a random number to determine whether or not the board should creak
		-- Now, tell the game that it needs to play the creaky board sound
		-- game:AudioManager.PlaySound("creakyboard")
		-- nextCreakTime = game:GameTime + 3
	--end