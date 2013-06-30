-- Load the room's events.
require("Data/Events/Foyer_events")

print("Foyer added to global table 'rooms'.")

rooms["Foyer"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/Foyer",
			Color = { 1, 1, 1, 1 },
			Scroll = { X = 0, Y = 0 },
			Type = "Background"
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "FrontDoor",
			Area = {
				X = 0,
				Y = 633,
				Width = 107,
				Height = 132
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 6,
				Y = 700
			},

			OnInteract = function()
				door("FrontDoor", "FrontYard", "FrontDoor")
			end,
			OnLook = nil
		},
		[2] = {
			Name = "HallDoor",
			Area = {
				X = 357,
				Y = 147,
				Width = 115,
				Height = 225
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 410,
				Y = 367
			},

			OnInteract = function()
				door("HallDoor", "LivingRoom", "Door")
			end,
			OnLook = nil
		},
		[3] = {
			Name = "Stairs",
			Polygon = {
				[1] = { X = 635, Y = 595 },
				[2] = { X = 835, Y = 354 },
				[3] = { X = 1024, Y = 378 },
				[4] = { X = 691, Y = 715 }
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 640,
				Y = 711
			},

			OnInteract = function()
				door("Stairs", "Upstairs", "Stairs")
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
				[1] = { X = 363, Y = 366 },
				[2] = { X = 460, Y = 366 },
				[3] = { X = 528, Y = 538 },
				[4] = { X = 294, Y = 538 }
			},
			[2] = {
				[1] = { X = 264, Y = 522 },
				[2] = { X = 561, Y = 522 },
				[3] = { X = 624, Y = 649 },
				[4] = { X = 198, Y = 649 }
			},
			[3] = {
				[1] = { X = 0, Y = 636 },
				[2] = { X = 615, Y = 636 },
				[3] = { X = 688, Y = 768 },
				[4] = { X = 0, Y = 768 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = { 
					[1] = { X = 316, Y = 526 },
					[2] = { X = 512, Y = 526 },
					[3] = { X = 507, Y = 526 }
				}
			},
			[2] = {
				Connects = { 2, 3 },
				Points = { 
					[1] = { X = 213, Y = 640 },
					[2] = { X = 414, Y = 640 },
					[3] = { X = 604, Y = 640 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "FrontDoor",
			
			X = 20,
			Y = 705
		},
		[2] = {
			Name = "HallDoor",
			
			X = 408,
			Y = 374
		},
		[3] = {
			Name = "Stairs",
			
			X = 634,
			Y = 704
		}
	}	
}
