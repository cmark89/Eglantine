-- Load the room's events if need be
require("Data/Events/this_room_events")

print("this_room added to global table 'rooms'.")

--Add an entry to the global "rooms" table
rooms["this_room"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/...",
			Color = { 1, 1, 1, 1 },
			Type = "Background" --Midground, Foreground
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Puzzlebox",
			Area = {
				X = 200,
				Y = 535,
				Width = 48,
				Height = 48
			},
				
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/...",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 300,
				Y = 658
			},

			OnInteract = function()
				 pickup("Puzzlebox")
			end,
			OnLook = lookAtPuzzlebox
		}
	},
	
	Triggers = {
		[1] = {
			Name = "CreakyBoard",
			X = 324,
			Y = 0,
			Width = 41,
			Height = 999,
			
			Enabled = true,
			
			OnEnter = creakyBoard
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 378, Y = 522 }
				-- ...
			}
			
			--[2]...	
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				
				Points = {
					[1] = { X = 382, Y = 613 },
					[2] = { X = 384, Y = 760 },
					[3] = { X = 384, Y = 601 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 755,
			Y = 535
		}
	}	
}