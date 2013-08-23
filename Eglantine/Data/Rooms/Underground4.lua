-- Load the room's events if need be
require("Data/Events/Underground4_events")

print("Underground4 added to global table 'rooms'.")

--Add an entry to the global "rooms" table
rooms["Underground4"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/Underground4",
			Color = { 1, 1, 1, 1 },
			Type = "Background" --Midground, Foreground
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Door",
			Area = {
				X = 17,
				Y = 0,
				Width = 84,
				Height = 515
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 118,
				Y = 518
			},

			OnInteract = function()
				Event:ShowMessage("...")
			end,
			OnLook = nil
		},
		
		[2] = {
			Name = "Artifact",
			Area = {
				X = 597,
				Y = 187,
				Width = 129,
				Height = 381
			},
				
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/artifact",
			YCutoff = 521,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[3] = {
			Name = "Grave",
			Area = {
				X = 472,
				Y = 416,
				Width = 243,
				Height = 244
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/grave",
			YCutoff = 521,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 463,
				Y = 639
			},

			OnInteract = interactWithGrave,
			OnLook = readHeadstone
		},
		[4] = {
			Name = "Flower1",
			Area = {
				X = 586,
				Y = 517,
				Width = 48,
				Height = 48
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/eglantine",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[5] = {
			Name = "Flower2",
			Area = {
				X = 613,
				Y = 533,
				Width = 48,
				Height = 48
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/eglantine",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[6] = {
			Name = "Flower3",
			Area = {
				X = 646,
				Y = 529,
				Width = 48,
				Height = 48
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/eglantine",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		}
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 49, Y = 553 },
				[2] = { X = 328, Y = 416 },
				[3] = { X = 533, Y = 314 },
				[4] = { X = 531, Y = 752 },
				[5] = { X = 302, Y = 754 },
				[6] = { X = 36, Y = 669 }
			},
			[2] = {
				[1] = { X = 519, Y = 409 },
				[2] = { X = 777, Y = 403 },
				[3] = { X = 988, Y = 519 },
				[4] = { X = 596, Y = 520 },
				[5] = { X = 514, Y = 565 }
			},
			[3] = {
				[1] = { X = 523, Y = 657 },
				[2] = { X = 594, Y = 650 },
				[3] = { X = 740, Y = 515 },
				[4] = { X = 993, Y = 510 },
				[5] = { X = 994, Y = 674 },
				[6] = { X = 791, Y = 748 },
				[7] = { X = 519, Y = 748 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = {1, 2},
				
				Points = {
					[1] = { X = 525, Y = 418 },
					[2] = { X = 524, Y = 475 },
					[3] = { X = 521, Y = 554 }
				}
			},
			[2] = {
				Connects = {1, 3},
				
				Points = {
					[1] = { X = 528, Y = 659 },
					[2] = { X = 528, Y = 700 },
					[3] = { X = 526, Y = 747 }
				}
			},
			[3] = {
				Connects = {2, 3},
				
				Points = {
					[1] = { X = 746, Y = 516 },
					[2] = { X = 824, Y = 516 },
					[3] = { X = 891, Y = 516 },
					[4] = { X = 972, Y = 516 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 63,
			Y = 606
		}
	},
	
	onEnter = beginEnding
}

