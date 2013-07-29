-- Load the room's events if need be
require("Data/Events/Underground2_events")

print("Underground2 added to global table 'rooms'.")

--Add an entry to the global "rooms" table
rooms["Underground2"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/underground2",
			Color = { 1, 1, 1, 1 },
			Type = "Background" --Midground, Foreground
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Rope",
			Area = {
				X = 57,
				Y = 599,
				Width = 166,
				Height = 114
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 234,
				Y = 648
			},

			OnInteract = function()
				door("Rope", "Underground3", "Rope")
			end,
			OnLook = function()
				Event:ShowMessage("How deep does this go?")
			end
		},
		[2] = {
			Name = "Door",
			Area = {
				X = 802,
				Y = 10,
				Width = 199,
				Height = 201
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 891,
				Y = 226
			},

			OnInteract = function()
				door("Door", "Underground1", "Door")
			end,
			OnLook = function()
				Event:ShowMessage("I don't think I could leave if I wanted to.")
			end
		}
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 812, Y = 202 },
				[2] = { X = 1001, Y = 191 },
				[3] = { X = 981, Y = 388 },
				[4] = { X = 927, Y = 534 },
				[5] = { X = 715, Y = 462 }
			},
			[2] = {
				[1] = { X = 722, Y = 455 },
				[2] = { X = 931, Y = 521 },
				[3] = { X = 811, Y = 667 },
				[4] = { X = 727, Y = 703 },
				[5] = { X = 600, Y = 554 }
			},
			[3] = {
				[1] = { X = 204, Y = 559 },
				[2] = { X = 606, Y = 548 },
				[3] = { X = 749, Y = 691 },
				[4] = { X = 588, Y = 734 },
				[5] = { X = 205, Y = 716 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				
				Points = {
					[1] = { X = 722, Y = 458 },
					[2] = { X = 804, Y = 484 },
					[3] = { X = 913, Y = 519 }
				}
			},
			[2] = {
				Connects = { 2, 3 },
				
				Points = {
					[1] = { X = 607, Y = 556 },
					[2] = { X = 663, Y = 614 },
					[3] = { X = 724, Y = 682 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 900,
			Y = 212
		},
		[2] = {
			Name = "Rope",
			
			X = 235,
			Y = 646
		}
	}	
}
