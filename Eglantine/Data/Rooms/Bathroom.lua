print("Bathroom added to global table 'rooms'.")

--Add an entry to the global "rooms" table
rooms["Bathroom"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/Bathroom",
			Color = { 1, 1, 1, 1 },
			Type = "Background" --Midground, Foreground
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Door",
			Polygon = {
				[1] = { X = 955, Y = 71 },
				[2] = { X = 1024, Y = 93},
				[3] = { X = 1024, Y = 683 },
				[4] = { X = 955, Y = 577 }
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 969,
				Y = 648
			},

			OnInteract = GameEvents.useBathroomDoor,
			Mouse = "Leave"
		},
		[2] = {
			Name = "CabinetClosed",
			Polygon = {
				[1] = { X = 871, Y = 58 },
				[2] = { X = 917, Y = 75 },
				[3] = { X = 918, Y = 283 },
				[4] = { X = 872, Y = 246 }
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 881,
				Y = 559
			},

			OnInteract = GameEvents.openCabinet,
			Mouse = "Hot"
		},
		[3] = {
			Name = "CabinetOpen",
			Polygon = {
				[1] = { X = 804, Y = 77 },
				[2] = { X = 870, Y = 52 },
				[3] = { X = 871, Y = 248 },
				[4] = { X = 805, Y = 276 }
			},
			DrawAt = { X = 783, Y = 40 },
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/BathroomCabinet",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 881,
				Y = 559
			},

			OnInteract = GameEvents.closeCabinet,
			OnLook = nil,
			Mouse = "Hot"
		},
		[4] = {
			Name = "DollHead",
			Area = {
				X = 875,
				Y = 101,
				Width = 35,
				Height = 46
			},
				
			Enabled = false,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 881,
				Y = 559
			},

			OnInteract = GameEvents.interactWithDollHead,
			OnLook = GameEvents.lookAtDollHead,
			Mouse = "Grab"
		},
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 235, Y = 544 },
				[2] = { X = 927, Y = 544 },
				[3] = { X = 1024, Y = 680 },
				[4] = { X = 1024, Y = 768 },
				[5] = { X = 82, Y = 768 }
			}
			
			--[2]...	
		},
		
		Connections = {
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 958,
			Y = 642
		}
	}	
}