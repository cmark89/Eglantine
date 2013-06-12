print("testroom added to global table 'rooms'.")

rooms["testroom"] = {
--------GRAPHICS--------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "EmptyRoom",
			Color = { 1, 1, 1, 1 },
			Scroll = { X = 0, Y = 0 },
			Type = "Background"
		}
	},
	--ForegroundTextures = { "Someotherfile", "Andanotherfordepth" },
	
--------OBJECTS AND EVENTS--------
	Interactables = {
		[1] = {
			Area = {
					X = 327,
					Y = 73,
					Width = 356,
					Height = 195
				},
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 305,
				Y = 593
			},

			OnInteract = lookAtWindow
		},
		
		[2] = {
			Area = {
					X = 808,
					Y = 310,
					Width = 48,
					Height = 48
				},
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 305,
				Y = 593
			},

			OnInteract = lookAtOutlet
		}		
	},
	
	Triggers = {
		[1] = {
			X = 324,
			Y = 609,
			Width = 41,
			Height = 159,
			
			OnEnter = stepOnCrack
		}
	},	
--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 133, Y = 403 },
				[2] = { X = 883, Y = 403 },
				[3] = { X = 1045, Y = 690 },
				[4] = { X = -15, Y = 690 }
			},
			
			[2] = {
				[1] = { X = 1020, Y = 652 },
				[2] = { X = 1020, Y = 763 },
				[3] = { X = 1, Y = 767},
				[4] = { X = 1, Y = 644}
			}			
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = {
					[1] = { X = 9, Y = 657 },
					[2] = { X = 507, Y = 660 },
					[3] = { X = 991, Y = 661 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
		
			Name = "Teleport",
			
			X = 500,
			Y = 600
		}
	}
}