#!/usr/local/bin/lua

function stepOnCrack()
	Event:ShowMessage("The floor is cracking!!")
end

function pillowClick()
	Event:MovePlayer(150, 175)
	Event:ShowMessage("That's a pillow, alright.")
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