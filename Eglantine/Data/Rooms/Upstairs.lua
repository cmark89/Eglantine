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
		["I1"] = {
			Name = "Stairs",
			Polygon = {
				[1] = { X = 730, Y = 768 },
				[2] = { X = 798, Y = 623 },
				[3] = { X = 970, Y = 629 },
				[4] = { X = 1024, Y = 768 }
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 751,
				Y = 683
			},

			OnInteract = GameEvents.useUpstairsDoor_Foyer,
			OnLook = nil,
			Mouse = "Leave"
		},
		["I2"] = {
			Name = "BedroomDoor",
			Polygon = {
				[1] = { X = 481, Y = 0 },
				[2] = { X = 590, Y = 0 },
				[3] = { X = 589, Y = 391 },
				[4] = { X = 482, Y = 476 },
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 544,
				Y = 439
			},

			OnInteract = GameEvents.useUpstairsDoor_Bedroom,
			OnLook = nil,
			Mouse = "Leave"
		},
		["I3"] = {
			Name = "EmptyRoomDoor",
			Area = {
				X = 678,
				Y = 1,
				Width = 119,
				Height = 378
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 731,
				Y = 368
			},

			OnInteract = GameEvents.useUpstairsDoor_EmptyRoom,
			OnLook = nil,
			Mouse = "Leave"
		},
		
		["I4"] = {
			Name = "Safe",
			Polygon = {
				[1] = { X = 306, Y = 92 },
				[2] = { X = 428, Y = 65 },
				[3] = { X = 430, Y = 225 },
				[4] = { X = 306, Y = 281 },
				
			},

			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 406,
				Y = 546
			},

			OnInteract = GameEvents.interactWithSafe,
			OnLook = GameEvents.lookAtSafe,
			Mouse = "Hot"
		},
		["I5"] = {
			Name = "OfficeDoor",
			Polygon = {
				[1] = { X = 0, Y = 0 },
				[2] = { X = 210, Y = 0 },
				[3] = { X = 210, Y = 685 },
				[4] = { X = 109, Y = 768 },
				[5] = { X = 0, Y = 768 }
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 169,
				Y = 733
			},

			OnInteract = GameEvents.useUpstairsDoor_Office,
			OnLook = nil,
			Mouse = "Leave"
		}
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 111, Y = 768 },
				[2] = { X = 626, Y = 368 },
				[3] = { X = 796, Y = 368 },
				[4] = { X = 798, Y = 624 },
				[5] = { X = 730, Y = 768 }
			}
		},
		
		Connections = { }
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Stairs",
			
			X = 758,
			Y = 682
		},
		[2] = {
			Name = "BedroomDoor",
			
			X = 544,
			Y = 440
		},
		[3] = {
			Name = "EmptyRoomDoor",
			
			X = 738,
			Y = 379
		},
		[4] = {
			Name = "OfficeDoor",
			
			X = 169,
			Y = 734
		}
	},
	
	onLoad = GameEvents.startIndoorSounds
}

