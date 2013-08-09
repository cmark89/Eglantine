-- Load the room's events.
require("Data/Events/LivingRoom_events")

print("LivingRoom added to global table 'rooms'.")

rooms["LivingRoom"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/LivingRoom",
			Color = { 1, 1, 1, 1 },
			Scroll = { X = 0, Y = 0 },
			Type = "Background"
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Painting",
			Polygon = {
				[1] = { X = 230, Y = 99 },
				[2] = { X = 369, Y = 83 },
				[3] = { X = 375, Y = 345 },
				[4] = { X = 231, Y = 372 },
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 324,
				Y = 424
			},

			OnInteract = interactWithPainting,
			OnLook = lookAtPainting
		},
		
		[2] = {
			Name = "Door",
			Polygon = {
				[1] = { X = 825, Y = 69 },
				[2] = { X = 976, Y = 73 },
				[3] = { X = 980, Y = 406 },
				[4] = { X = 817, Y = 387 },
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 895,
				Y = 408
			},

			OnInteract = function ()
				door("Door", "Foyer", "LivingRoomDoor")
			end,
			OnLook = nil
		},
		[3] = {
			Polygon = {
				[1] = { X = 595, Y = 262 },
				[2] = { X = 778, Y = 273 },
				[3] = { X = 773, Y = 397 },
				[4] = { X = 592, Y = 373 }
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 666,
				Y = 473
			},

			OnInteract = interactWithTV,
			OnLook = lookAtTV
		},
		[4] = {
			Name = "Static1",
			Area = {
					X = 604,
					Y = 239,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/static1",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[5] = {
			Name = "Static2",
			Area = {
					X = 604,
					Y = 239,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/static2",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[6] = {
			Name = "Static3",
			Area = {
					X = 604,
					Y = 239,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/static3",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[7] = {
			Name = "Static4",
			Area = {
					X = 604,
					Y = 239,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/static4",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[8] = {
			Name = "Static5",
			Area = {
					X = 604,
					Y = 239,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/static5",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[9] = {
			Name = "Static6",
			Area = {
					X = 604,
					Y = 239,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/static6",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[10] = {
			Name = "Tear",
			Area = {
					X = 248,
					Y = 99,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/LivingRoomTear",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		}
	},
	
	Triggers = {
		[1] = {
			Name = "TVActivate",
			Area = {
				X = 452,
				Y = 366,
				Width = 51,
				Height = 422
			},
			Enabled = true,
		
			OnEnter = turnOnTV
		}
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 1024, Y = 411 },
				[2] = { X = 571, Y = 361 },
				[3] = { X = 0, Y = 498 },
				[4] = { X = 0, Y = 768 },
				[5] = { X = 1024, Y = 768 }
			}
		},
		
		Connections = {
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 892,
			Y = 409
		},
		
		[2] = {
			Name = "Painting",
			
			X = 316,
			Y = 427
		}
	},
	
	onEnter = checkTV,
	onLoad = checkTV
}