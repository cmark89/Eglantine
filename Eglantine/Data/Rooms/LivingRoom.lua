-- Load the room's events.
require("Data/Events/LivingRoom_events")

print("LivingRoom added to global table 'rooms'.")

rooms["LivingRoom"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/LivingRoom",
			Color = { 1, 1, 1, 1 },
			Scroll = { X = 0, Y = 0 },
			Type = "Background"
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Painting",
			Polygon = {
				[1] = { X = 230, Y = 99 },
				[2] = { X = 369, Y = 83 },
				[3] = { X = 375, Y = 345 },
				[4] = { X = 231, Y = 372 },
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 324,
				Y = 424
			},

			OnInteract = interactWithPainting,
			OnLook = lookAtPainting
		},
		
		[2] = {
			Name = "Door",
			Polygon = {
				[1] = { X = 825, Y = 69 },
				[2] = { X = 976, Y = 73 },
				[3] = { X = 980, Y = 406 },
				[4] = { X = 817, Y = 387 },
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 895,
				Y = 408
			},

			OnInteract = function ()
				door("Door", "Foyer", "HallDoor")
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
				[1] = { X = 1024, Y = 411 },
				[2] = { X = 571, Y = 361 },
				[3] = { X = 0, Y = 498 },
				[4] = { X = 0, Y = 768 },
				[5] = { X = 1024, Y = 768 }
			}
		},
		
		Connections = {
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 892,
			Y = 409
		},
		
		[2] = {
			Name = "Painting",
			
			X = 316,
			Y = 427
		}
	}	
}