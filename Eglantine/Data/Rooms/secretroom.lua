-- Load the room's events.
require("Data/Events/secretroom_events")

print("secretroom added to global table 'rooms'.")

rooms["secretroom"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/secretroom",
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
				door("Door", "testroom", "Teleport")
			end,
			OnLook = nil
		}
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 378, Y = 522 },
				[2] = { X = 1024, Y = 522 },
				[3] = { X = 1024, Y = 768 },
				[4] = { X = 378, Y = 768 }
			},
			
			[2] = {
				[1] = { X = 204, Y = 768 },
				[2] = { X = 204, Y = 706 },
				[3] = { X = 393, Y = 600 },
				[4] = { X = 393, Y = 768 }
			}
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
		},
		
		[2] = {
			Name = "Trapdoor",
			
			X = 790,
			Y = 718
		}
	}	
}