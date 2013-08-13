-- Load the room's events.
require("Data/Events/Upstairs_events")

print("Upstairs added to global table 'rooms'.")

rooms["Upstairs"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/UpstairsHall",
			Color = { 1, 1, 1, 1 },
			Scroll = { X = 0, Y = 0 },
			Type = "Background"
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Stairs",
			Area = {
				X = 597,
				Y = 134,
				Width = 189,
				Height = 282
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 692,
				Y = 428
			},

			OnInteract = function()
				door("Stairs", "Foyer", "Stairs")
			end,
			OnLook = nil
		},
		[2] = {
			Name = "BedroomDoor",
			Area = {
				X = 0,
				Y = 466,
				Width = 91,
				Height = 151
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 80,
				Y = 550
			},

			OnInteract = function()
				door("BedroomDoor", "Bedroom", "HallDoor")
			end,
			OnLook = nil
		},
		[3] = {
			Name = "EmptyRoomDoor",
			Polygon = {
				[1] = { X = 1024, Y = 768 },
				[2] = { X = 844, Y = 576 },
				[3] = { X = 1024, Y = 568 }
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 982,
				Y = 620
			},

			OnInteract = function()
				door("EmptyRoomDoor", "EmptyRoom", "Door")
			end,
			OnLook = nil
		},
		
		[4] = {
			Name = "Safe",
			Area = {
				X = 69,
				Y = 234,
				Width = 118,
				Height = 93
			},

			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 132,
				Y = 478
			},

			OnInteract = interactWithSafe,
			OnLook = function()
				Event:ShowMessage("That safe looks pretty sturdy.")
			end
		},
		[5] = {
			Name = "OfficeDoor",
			Area = {
				X = 237,
				Y = 219,
				Width = 170,
				Height = 228
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 327,
				Y = 460
			},

			OnInteract = function()
				door("OfficeDoor", "Office", "Door")
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
				[1] = { X = 0, Y = 468 },
				[2] = { X = 591, Y = 424 },
				[3] = { X = 1024, Y = 391 },
				[4] = { X = 1024, Y = 561 },
				[5] = { X = 0, Y = 615 }
			},
			[2] = {
				[1] = { X = 828, Y = 558 },
				[2] = { X = 1024, Y = 544 },
				[3] = { X = 1024, Y = 741 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = { 
					[1] = { X = 850, Y = 561 },
					[2] = { X = 927, Y = 558 },
					[3] = { X = 997, Y = 552 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Stairs",
			
			X = 699,
			Y = 523
		},
		[2] = {
			Name = "BedroomDoor",
			
			X = 82,
			Y = 546
		},
		[3] = {
			Name = "EmptyRoomDoor",
			
			X = 967,
			Y = 594
		},
		[4] = {
			Name = "OfficeDoor",
			
			X = 322,
			Y = 466
		}
	}	
}

