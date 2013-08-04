-- Load the room's events.
require("Data/Events/BackYard_events")

print("BackYard added to global table 'rooms'.")

rooms["BackYard"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/backyard",
			Color = { 1, 1, 1, 1 },
			Scroll = { X = 0, Y = 0 },
			Type = "Background"
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Door",
			Polygon = {
				[1] = { X = 922, Y = 157},
				[2] = { X = 1024, Y = 177},
				[3] = { X = 1024, Y = 591},
				[4] = { X = 855, Y = 456}
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 924,
				Y = 535
			},

			OnInteract = function()
				--Real temporary
				door("Door", "Kitchen", "BackYardDoor")
			end,
			OnLook = nil
		},
		[2] = {
			Name = "Crowbar",
			Area = {
				X = 245,
				Y = 490,
				Width = 48,
				Height = 48
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/crowbar",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 231,
				Y = 544
			},

			OnInteract = function()
				pickup("Crowbar")
			end,
			OnLook = function()
				Event:ShowMessage("That looks like it may be useful.")	
			end
		},
		[3] = {
			Name = "Eglantine",
			Area = {
				X = 235,
				Y = 102,
				Width = 48,
				Height = 48
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/eglantine",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 259,
				Y = 210
			},

			OnInteract = function()
				pickBackYardEglantine()
			end,
			OnLook = function()
				Event:ShowMessage("Sweet briar...never really cared for it.")	
			end
		}
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 684, Y = 349 },
				[2] = { X = 1024, Y = 588 },
				[3] = { X = 1024, Y = 768 },
				[4] = { X = 211, Y = 768 },
				[5] = { X = 261, Y = 571 },
			},
			[2] = {
				[1] = { X = 0, Y = 234 },
				[2] = { X = 231, Y = 340 },
				[3] = { X = 243, Y = 502 },
				[4] = { X = 285, Y = 553 },
				[5] = { X = 240, Y = 768 },
				[6] = { X = 0, Y = 768 }
			},
			[3] = {
				[1] = { X = 67, Y = 274 },
				[2] = { X = 192, Y = 204 },
				[3] = { X = 510, Y = 171 },
				[4] = { X = 561, Y = 213 },
				[5] = { X = 559, Y = 286 },
				[6] = { X = 285, Y = 390 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = { 
					[1] = { X = 271, Y = 573 },
					[2] = { X = 249, Y = 658 },
					[3] = { X = 228, Y = 741 }
				}
			},
			[2] = {
				Connects = { 2, 3 },
				Points = { 
					[1] = { X = 85, Y = 280 },
					[2] = { X = 148, Y = 309 },
					[3] = { X = 208, Y = 345 }
				}
			},
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 924,
			Y = 535
		}
	}
}