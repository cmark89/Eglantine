-- Load the room's events if need be
require("Data/Events/Underground1_events")

print("Underground1 added to global table 'rooms'.")

--Add an entry to the global "rooms" table
rooms["Underground1"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/underground1",
			Color = { 1, 1, 1, 1 },
			Type = "Background" --Midground, Foreground
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Rope",
			Area = {
				X = 166,
				Y = 56,
				Width = 37,
				Height = 448
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 174,
				Y = 509
			},

			OnInteract = function()
				door("Rope", "SecretRoom", "Trapdoor")
			end,
			OnLook = function()
				Event:ShowMessage("Maybe I should get out of here...")
			end
		},
		[2] = {
			Name = "Door",
			Area = {
				X = 824,
				Y = 329,
				Width = 132,
				Height = 250
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 884,
				Y = 588
			},

			OnInteract = function()
				door("Door", "Underground2", "Door")
			end,
			OnLook = function()
				Event:ShowMessage("I have a bad feeling about this place...")
			end
		}
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 43, Y = 538 },
				[2] = { X = 93, Y = 493 },
				[3] = { X = 191, Y = 486 },
				[4] = { X = 263, Y = 528 }
			},
			[2] = {
				[1] = { X = 0, Y = 535 },
				[2] = { X = 257, Y = 515 },
				[3] = { X = 648, Y = 509 },
				[4] = { X = 732, Y = 517 },
				[5] = { X = 742, Y = 659 },
				[6] = { X = 28, Y = 656 },
				[7] = { X = 0, Y = 631 }
			},
			[3] = {
				[1] = { X = 711, Y = 515 },
				[2] = { X = 793, Y = 537 },
				[3] = { X = 975, Y = 585 },
				[4] = { X = 1020, Y = 584 },
				[5] = { X = 1020, Y = 661 },
				[6] = { X = 718, Y = 663 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				
				Points = {
					[1] = { X = 61, Y = 533 },
					[2] = { X = 144, Y = 529 },
					[3] = { X = 328, Y = 521 }
				}
			},
			[2] = {
				Connects = { 2, 3 },
				
				Points = {
					[1] = { X = 723, Y = 530 },
					[2] = { X = 723, Y = 585 },
					[3] = { X = 723, Y = 647 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 869,
			Y = 581
		},
		[2] = {
			Name = "Rope",
			
			X = 167,
			Y = 529
		}
	}	
}
