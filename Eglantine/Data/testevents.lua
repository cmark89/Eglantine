#!/usr/local/bin/lua

function lookAtWindow()
	Event:ShowMessage("I can see my house from here!")
	Event:ShowMessage("Just kidding.  That's what you call a joke.")
end

function lookAtOutlet()
	Event:ShowMessage("What a shitty place to live...")
end


function creakyBoard()
	-- if gameTime < nextCreakTime then do
		-- return
	-- else do
		-- Get a random number to determine whether or not the board should creak
		-- Now, tell the game that it needs to play the creaky board sound
		-- game:AudioManager.PlaySound("creakyboard")
		-- nextCreakTime = game:GameTime + 3
	--end
end