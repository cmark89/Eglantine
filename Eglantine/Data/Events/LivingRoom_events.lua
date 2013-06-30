function lookAtPainting()
	if GameState.PaintingOpened then
		Event:ShowMessage("What a waste of a painting...")
	else
		Event:ShowMessage("What a magnificent creature!")
	end
end

function interactWithPainting()
	if GameState.PaintingOpened then
		door("Painting", "SecretRoom", "Door")
	else
		if Event:UsingItem("Scissors") then
			runCoroutine(function()
				Event:MovePlayerTo("Painting")
				waitUntil("Player stopped")
				--Here, the player does some cool cutting of the painting
				Event:PlaySound("Extend")
				GameState.PaintingOpened = true
				--Disable the painting layer
			end)
		end
	end
end