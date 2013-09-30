print("BackYard added to global table 'rooms'.")

rooms["BackYard"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/BackYard",
			Color = { 1, 1, 1, 1 },
			Scroll = { X = 0, Y = 0 },
			Type = "Background"
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		["I1"] = {
			Name = "Door",
			Polygon = {
				[1] = { X = 27, Y = 159},
				[2] = { X = 210, Y = 57},
				[3] = { X = 214, Y = 559},
				[4] = { X = 25, Y = 717}
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 145,
				Y = 652
			},

			OnInteract = GameEvents.useBackYardDoor,
			
			Mouse = "Leave"
		},
		["I2"] = {
			Name = "Crowbar",
			Area = {
				X = 872,
				Y = 454,
				Width = 48,
				Height = 48
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/crowbar",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 850,
				Y = 500
			},

			OnInteract = GameEvents.pickUpCrowbar,
			OnLook = GameEvents.lookAtCrowbar,
			
			Mouse = "Grab"
		},
		["I3"] = {
			Name = "Eglantine",
			Area = {
				X = 726,
				Y = 134,
				Width = 48,
				Height = 48
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/BackyardFlower",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 721,
				Y = 476
			},

			OnInteract = GameEvents.pickBackYardEglantine,
			OnLook = GameEvents.lookAtBackYardFlower,
			
			Mouse = "Hot"
		}
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 295, Y = 504 },
				[2] = { X = 583, Y = 448 },
				[3] = { X = 805, Y = 483 },
				[4] = { X = 1024, Y = 534 },
				[5] = { X = 1024, Y = 768 },
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
			
			X = 157,
			Y = 642
		}
	},
	
	onEnter = GameEvents.enterBackYard,
	onExit = GameEvents.leaveBackYard,
	onLoad = GameEvents.startOutdoorSounds
}