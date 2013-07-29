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
				X = 934,
				Y = 548,
				Width = 48,
				Height = 48
			},
				
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/puzzlebox",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 906,
				Y = 644
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
			Name = "TrapdoorClosed",
			Polygon = {
				[1] = { X = 221, Y = 543 },
				[2] = { X = 379, Y = 545 },
				[3] = { X = 375, Y = 689 },
				[4] = { X = 185, Y = 683 }
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 402,
				Y = 631
			},

			OnInteract = interactWithTrapdoor,
			OnLook = function()
				Event:ShowMessage("Wonder what's down there...")
			end
		},
		[4] = {
			Name = "TrapdoorOpenGraphic",
			Area = {
				X = 176,
				Y = 391,
				Width = 0,
				Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/trapdooropen",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 888,
				Y = 645
			},

			OnInteract = nil,
			OnLook = nil
		},
		[5] = {
			Name = "TrapdoorOpen",
			Polygon = {
				[1] = { X = 221, Y = 543 },
				[2] = { X = 379, Y = 545 },
				[3] = { X = 375, Y = 689 },
				[4] = { X = 185, Y = 683 }
			},
				
			Enabled = false,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 402,
				Y = 631
			},

			OnInteract = function()
				door("TrapdoorOpen", "Underground1", "Rope")
			end,
			
			OnLook = function()
				Event:ShowMessage("Looks pretty dark down there...")
			end
		},
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 384, Y = 522 },
				[2] = { X = 870, Y = 522 },
				[3] = { X = 876, Y = 613 },
				[4] = { X = 1020, Y = 717 },
				[5] = { X = 1024, Y = 768 },
				[6] = { X = 384, Y = 768 }
			},
			[2] = {
				[1] = { X = 154, Y = 684 },
				[2] = { X = 411, Y = 691 },
				[3] = { X = 414, Y = 768 },
				[4] = { X = 127, Y = 768 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = { 
					[1] = { X = 394, Y = 700 },
					[2] = { X = 249, Y = 724 },
					[3] = { X = 394, Y = 750 }
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
			
			X = 401,
			Y = 631
		}
	}	
}