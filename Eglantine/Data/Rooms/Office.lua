-- Load the room's events.
require("Data/Events/Office_events")

print("Office added to global table 'rooms'.")

rooms["Office"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/office",
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
				[1] = {X=0, Y=171},
				[2] = {X=67, Y=178},
				[3] = {X=96, Y=445},
				[4] = {X=0, Y=529}
			},
			
			Enabled = true,
			Drawn = false,
			
			InteractPoint = {
				X = 56,
				Y = 498
			},
			
			OnInteract = function()
				door("Door", "Upstairs", "OfficeDoor")
			end,
			OnLook = nil
		},
		[2] = {
			Name = "TopDrawer_Open",
			Polygon = {
				[1] = {X=348, Y=131},
				[2] = {X=452, Y=136},
				[3] = {X=442, Y=190},
				[4] = {X=412, Y=207},
				[5] = {X=330, Y=210},
				[6] = {X=324, Y=170}
			},
			
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/opendrawer",
			DrawAt = { X = 324, Y = 129},
			
			InteractPoint = {
				X = 380,
				Y = 394
			},
			
			OnInteract = closeTopDrawer,
			OnLook = nil
		},
		[3] = {
			Name = "BottomDrawer_Open",
			Polygon = {
				[1] = {X=318, Y=290},
				[2] = {X=348, Y=252},
				[3] = {X=448, Y=255},
				[4] = {X=440, Y=311},
				[5] = {X=409, Y=329},
				[6] = {X=328, Y=331}
			},
			
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/opendrawer",
			DrawAt = { X = 321, Y = 245},
			
			InteractPoint = {
				X = 380,
				Y = 394
			},
			
			OnInteract = closeBottomDrawer,
			OnLook = nil
		},
		[4] = {
			Name = "TopDrawer_Closed",
			Area = {
				X = 351,
				Y = 131,
				Width = 98,
				Height = 66
			},
			
			Enabled = true,
			Drawn = false,
			
			InteractPoint = {
				X = 380,
				Y = 394
			},
			
			OnInteract = openTopDrawer,
			OnLook = nil
		},
		[5] = {
			Name = "BottomDrawer_Closed",
			Area = {
				X = 359,
				Y = 249,
				Width = 84,
				Height = 78
			},
			
			Enabled = true,
			Drawn = false,
			
			InteractPoint = {
				X = 380,
				Y = 394
			},
			
			OnInteract = openBottomDrawer,
			OnLook = nil
		},
		[5] = {
			Name = "BottomDrawer_Closed",
			Area = {
				X = 359,
				Y = 249,
				Width = 84,
				Height = 78
			},
			
			Enabled = true,
			Drawn = false,
			
			InteractPoint = {
				X = 380,
				Y = 394
			},
			
			OnInteract = openBottomDrawer,
			OnLook = nil
		},
		[6] = {
			Name = "Strange Notes",
			Area = {
				X = 342,
				Y = 150,
				Width = 48,
				Height = 30
			},
			
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/document1cropped",
			
			InteractPoint = {
				X = 380,
				Y = 394
			},
			
			OnInteract = function()
				pickup("Strange Notes")
			end,
			OnLook = nil
		},
		[7] = {
			Name = "Blueprints",
			Area = {
				X = 401,
				Y = 140,
				Width = 25,
				Height = 30
			},
			
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/blueprintscropped",
			
			InteractPoint = {
				X = 380,
				Y = 394
			},
			
			OnInteract = function()
				pickup("Blueprints")
			end,
			OnLook = nil
		},
		[8] = {
			Name = "Photograph",
			Area = {
				X = 342,
				Y = 262,
				Width = 48,
				Height = 30
			},
			
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/photocropped",
			
			InteractPoint = {
				X = 380,
				Y = 394
			},
			
			OnInteract = function()
				pickup("Photograph")
			end,
			OnLook = nil
		},
		[9] = {
			Name = "Journal",
			Area = {
				X = 386,
				Y = 262,
				Width = 48,
				Height = 30
			},
			
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/bookcropped",
			
			InteractPoint = {
				X = 380,
				Y = 394
			},
			
			OnInteract = function()
				pickup("Journal")
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
				[1] = { X = 0, Y = 534 },
				[2] = { X = 184, Y = 369 },
				[3] = { X = 1024, Y = 411 },
				[4] = { X = 1024, Y = 768 },
				[5] = { X = 0, Y = 768 },
			}
		},
		
		Connections = {
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 57,
			Y = 486
		}
	}
}