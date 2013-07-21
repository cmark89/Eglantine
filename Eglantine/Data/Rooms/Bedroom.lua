-- Load the room's events.
require("Data/Events/Bedroom_events")

print("Bedroom added to global table 'rooms'.")

rooms["Bedroom"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/bedroom",
			Color = { 1, 1, 1, 1 },
			Scroll = { X = 0, Y = 0 },
			Type = "Background"
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "HallDoor",
			Polygon = {
				[1] = { X = 840, Y = 15},
				[2] = { X = 976, Y = 46},
				[3] = { X = 931, Y = 286},
				[4] = { X = 790, Y = 240}
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 856,
				Y = 268
			},

			OnInteract = function()
				door("HallDoor", "Upstairs", "BedroomDoor")
			end,
			OnLook = nil
		},
		[2] = {
			Name = "BathroomDoor",
			Polygon = {
				[1] = { X = 147, Y = 127},
				[2] = { X = 253, Y = 78},
				[3] = { X = 319, Y = 307},
				[4] = { X = 162, Y = 397}
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 255,
				Y = 363
			},

			OnInteract = function()
				door("BathroomDoor", "Bathroom", "Door")
			end,
			OnLook = nil
		},
		[3] = {
			Name = "Scissors",
			Area = {
				X = 115,
				Y = 595,
				Width = 48,
				Height = 48
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/scissors",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 154,
				Y = 603
			},

			OnInteract = function()
				pickup("Scissors")
			end,
			OnLook = function()
				Event:ShowMessage("Good God, what the hell is that...?")
			end
		},
		[4] = {
			Name = "Trashcan",
			Area = {
				X = 295,
				Y = 421,
				Width = 183,
				Height = 82
			},
			
			Enabled = true,
			Drawn = false,
			
			InteractPoint = {
				X = 154,
				Y = 603
			},

			OnInteract = nil,
			OnLook = function()
				Event:ShowMessage("What a mess...")
			end
		},
		
		[5] = {
			Name = "Letter",
			Area = {
				X = 367,
				Y = 453,
				Width = 48,
				Height = 48
			},
			
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/foldednote",
			
			InteractPoint = {
				X = 370,
				Y = 507
			},

			OnInteract = function()
				pickup("Letter")
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
				[1] = { X = 679, Y = 193 },
				[2] = { X = 1024, Y = 307 },
				[3] = { X = 1024, Y = 790 },
				[4] = { X = 522, Y = 402 }
			},
			[2] = {
				[1] = { X = 325, Y = 307 },
				[2] = { X = 537, Y = 388 },
				[3] = { X = 1024, Y = 768 },
				[4] = { X = 0, Y = 768 },
				[5] = { X = 0, Y = 522 },
				[6] = { X = 151, Y = 408 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = { 
					[1] = { X = 555, Y = 408 },
					[2] = { X = 666, Y = 492 },
					[3] = { X = 756, Y = 565 },
					[4] = { X = 823, Y = 615 },
					[5] = { X = 892, Y = 670 },
					[6] = { X = 966, Y = 729 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "HallDoor",
			
			X = 856,
			Y = 270
		},
		[2] = {
			Name = "BathroomDoor",
			
			X = 255,
			Y = 363
		}
	}	
}

