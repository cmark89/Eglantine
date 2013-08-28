-- Load the room's events if need be
require("Data/Events/Kitchen_events")

print("Kitchen added to global table 'rooms'.")

--Add an entry to the global "rooms" table
rooms["Kitchen"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/KitchenBase",
			Color = { 1, 1, 1, 1 },
			Type = "Background" --Midground, Foreground
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "WindowLayer",
			Area = {
				X = 0,
				Y = 0,
				Width = 1024,
				Height = 768
			},
				
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Rooms/KitchenWindow",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[2] = {
			Name = "BrokenWindowLayer",
			Area = {
				X = 0,
				Y = 0,
				Width = 1024,
				Height = 768
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Rooms/KitchenWindowBroken",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[3] = {
			Name = "BackYardDoor",
			Polygon = {
				[1] = { X = 450, Y = 112 },
				[2] = { X = 604, Y = 106 },
				[3] = { X = 605, Y = 316 },
				[4] = { X = 531, Y = 345 },
				[5] = { X = 531, Y = 431 },
				[6] = { X = 457, Y = 414 }
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 486,
				Y = 457
			},

			OnInteract = function()
				door("BackYardDoor", "BackYard", "Door")
			end,
			OnLook = nil,
			Mouse = "Leave"
		},
		[4] = {
			Name = "FoyerDoor",
			Polygon = {
				[1] = { X = 68, Y = 134 },
				[2] = { X = 242, Y = 138 },
				[3] = { X = 247, Y = 504 },
				[4] = { X = 68, Y = 574 }
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 161,
				Y = 550
			},

			OnInteract = function()
				door("FoyerDoor", "Foyer", "KitchenDoor")
			end,
			OnLook = nil,
			Mouse = "Leave"
		},
		[5] = {
			Name = "Eglantine",
			Area = {
				X = 557,
				Y = 343,
				Width = 30,
				Height = 20
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/KitchenFlower",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 566,
				Y = 505
			},

			OnInteract = pickKitchenEglantine,
			OnLook = function()
				Event:ShowMessage("Okay, this is getting pretty weird...")
			end,
			Mouse = "Hot"
		}
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 0, Y = 602 },
				[2] = { X = 457, Y = 414 },
				[3] = { X = 529, Y = 432 },
				[4] = { X = 538, Y = 768 }
			},
			[2] = {
				[1] = { X = 527, Y = 485 },
				[2] = { X = 1024, Y = 624 },
				[3] = { X = 1024, Y = 768 },
				[4] = { X = 527, Y = 768 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				
				Points = {
					[1] = { X = 529, Y = 490 },
					[2] = { X = 529, Y = 561 },
					[3] = { X = 529, Y = 610 },
					[4] = { X = 529, Y = 680 },
					[5] = { X = 529, Y = 755 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "FoyerDoor",
			
			X = 178,
			Y = 550
		},
		[2] = {
			Name = "BackYardDoor",
			
			X = 496,
			Y = 439
		}
	},
	
	onEnter = checkWindow,
	onExit = leaveKitchen
}