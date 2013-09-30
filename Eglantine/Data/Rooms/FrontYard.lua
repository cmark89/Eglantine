print("FrontYard added to global table 'rooms'.")

rooms["FrontYard"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/FrontYard",
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
				[1] = { X = 522, Y = 82 },
				[2] = { X = 763, Y = 85 },
				[3] = { X = 763, Y = 529 },
				[4] = { X = 522, Y = 541 },
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 657,
				Y = 555
			},

			OnInteract = GameEvents.useFrontYardDoor,
			OnLook = GameEvents.lookAtFrontDoor,
			Mouse = "Leave"
		},
		["I2"] = {
			Name = "Eglantine",
			Area = {
				X = 365,
				Y = 600,
				Width = 29,
				Height = 18
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/FrontYardFlower",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 508,
				Y = 682
			},

			OnInteract = GameEvents.pickFrontYardEglantine,
			OnLook = GameEvents.lookAtFrontYardFlower,
			Mouse = "Hot"
		}
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 0, Y = 732 },
				[2] = { X = 522, Y = 538 },
				[3] = { X = 910, Y = 543 },
				[4] = { X = 1024, Y = 576 },
				[5] = { X = 1024, Y = 768 },
				[6] = { X = 0, Y = 768 }
			}
		},
		
		Connections = {
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "FrontDoor",
			
			X = 646,
			Y = 562
		}
	},
	
	onEnter = GameEvents.enterFrontYard,
	onExit = GameEvents.leaveFrontYard,
	onLoad = GameEvents.startOutdoorSounds	
}