-- Load the room's events.
require("Data/Events/SecretRoom_events")

print("SecretRoom added to global table 'rooms'.")

rooms["SecretRoom"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/SecretRoom",
			Color = { 1, 1, 1, 1 },
			Scroll = { X = 0, Y = 0 },
			Type = "Background"
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
			Texture = "Graphics/Objects/puzzlebox",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 300,
				Y = 658
			},

			OnInteract = function()
				 pickup("Puzzlebox")
			end,
			OnLook = lookAtPuzzlebox
		},
		
		[2] = {
			Name = "Door",
			Area = {
				X = 672,
				Y = 172,
				Width = 200,
				Height = 283
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 756,
				Y = 536
			},

			OnInteract = function ()
				door("Door", "LivingRoom", "Painting")
			end,
			OnLook = function()
				Event:ShowMessage("Waste of a good painting...")
			end
		},
		
		[3] = {
			Name = "Trapdoor",
			Polygon = {
				[1] = { X = 801, Y = 676 },
				[2] = { X = 1024, Y = 640 },
				[3] = { X = 1024, Y = 678 },
				[4] = { X = 894, Y = 765 },
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 888,
				Y = 645
			},

			OnInteract = nil,
			
			OnLook = function()
				Event:ShowMessage("That trapdoor looks sealed up tight.  If I had the key to it, though...")
			end
		}
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 1024, Y = 525 },
				[2] = { X = 381, Y = 525 },
				[3] = { X = 381, Y = 613 },
				[4] = { X = 198, Y = 712 },
				[5] = { X = 198, Y = 768 },
				[6] = { X = 0, Y = 768 },
				[7] = { X = 1024, Y = 768 }
			}
		},
		
		Connections = {
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 755,
			Y = 535
		},
		
		[2] = {
			Name = "Trapdoor",
			
			X = 790,
			Y = 718
		}
	}	
}