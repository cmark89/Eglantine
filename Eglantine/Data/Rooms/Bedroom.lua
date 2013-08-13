-- Load the room's events.
require("Data/Events/Bedroom_events")

print("Bedroom added to global table 'rooms'.")

rooms["Bedroom"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/Bedroom",
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
				[1] = { X = 834, Y = 0},
				[2] = { X = 900, Y = 0},
				[3] = { X = 898, Y = 377},
				[4] = { X = 848, Y = 377},
				[5] = { X = 834, Y = 360}
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 839,
				Y = 376
			},

			OnInteract = function()
				door("HallDoor", "Upstairs", "BedroomDoor")
			end,
			OnLook = nil
		},
		[2] = {
			Name = "BathroomDoor",
			Area = {
				X = 556,
				Y = 0,
				Width = 180,
				Height = 310
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 640,
				Y = 321
			},

			OnInteract = function()
				door("BathroomDoor", "Bathroom", "Door")
			end,
			OnLook = nil
		},
		[3] = {
			Name = "Scissors",
			Area = {
				X = 406,
				Y = 598,
				Width = 48,
				Height = 48
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/scissors",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 426,
				Y = 665
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
				X = 114,
				Y = 386,
				Width = 172,
				Height = 135
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
				X = 200,
				Y = 467,
				Width = 48,
				Height = 48
			},
			
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/foldednote",
			
			InteractPoint = {
				X = 252,
				Y = 511
			},

			OnInteract = function()
				pickup("Letter")
			end,
			
			OnLook = nil
		},
		
		[6] = {
			Name = "Bed",
			Area = {
				X = 614,
				Y = 367,
				Width = 0,
				Height = 0
			},
			
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/bed",
			YCutoff = 418,
			
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 342, Y = 308 },
				[2] = { X = 808, Y = 310 },
				[3] = { X = 868, Y = 418 },
				[4] = { X = 244, Y = 420 }
			},
			[2] = {
				[1] = { X = 266, Y = 407 },
				[2] = { X = 632, Y = 407 },
				[3] = { X = 648, Y = 768 },
				[4] = { X = 226, Y = 768 },
				[5] = { X = 226, Y = 497 }
			},
			[3] = {
				[1] = { X = 99, Y = 416 },
				[2] = { X = 238, Y = 547 },
				[3] = { X = 235, Y = 768 },
				[4] = { X = 0, Y = 768 },
				[5] = { X = 0, Y = 716 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = { 
					[1] = { X = 269, Y = 414 },
					[2] = { X = 312, Y = 414 },
					[3] = { X = 367, Y = 414 },
					[4] = { X = 412, Y = 414 },
					[5] = { X = 502, Y = 414 },
					[6] = { X = 533, Y = 414 },
					[7] = { X = 574, Y = 414 },
					[8] = { X = 625, Y = 414 }
				}
			},
			
			[2] = {
				Connects = { 2, 3 },
				Points = {
					[1] = { X = 232, Y = 552 },
					[2] = { X = 232, Y = 599 },
					[3] = { X = 232, Y = 642 },
					[4] = { X = 232, Y = 692 },
					[5] = { X = 232, Y = 746 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "HallDoor",
			
			X = 837,
			Y = 378
		},
		[2] = {
			Name = "BathroomDoor",
			
			X = 641,
			Y = 322
		}
	}	
}

