print("Underground2 added to global table 'rooms'.")

--Add an entry to the global "rooms" table
rooms["Underground2"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/Underground2",
			Color = { 1, 1, 1, 1 },
			Type = "Background" --Midground, Foreground
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "RopeDown",
			Polygon = {
				[1] = { X = 416, Y = 582 },
				[2] = { X = 497, Y = 607 },
				[3] = { X = 535, Y = 661 },
				[4] = { X = 489, Y = 689 },
				[5] = { X = 478, Y = 713 },
				[6] = { X = 473, Y = 768 },
				[7] = { X = 403, Y = 768 },
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 510,
				Y = 603
			},

			OnInteract = GameEvents.useUnderground2Door_Down,
			Mouse = "Leave"
		},
		[2] = {
			Name = "RopeUp",
			Area = {
				X = 862,
				Y = 2,
				Width = 137,
				Height = 455
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 935,
				Y = 453
			},

			OnInteract = GameEvents.useUnderground2Door_Down,
			Mouse = "Leave"
		}
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 864, Y = 440 },
				[2] = { X = 1024, Y = 455 },
				[3] = { X = 1024, Y = 652 },
				[4] = { X = 910, Y = 702 },
				[5] = { X = 853, Y = 731 },
				[6] = { X = 536, Y = 649 },
				[7] = { X = 417, Y = 583 },
				[8] = { X = 541, Y = 517 }
			},
			[2] = {
				[1] = { X = 568, Y = 644 },
				[2] = { X = 851, Y = 718 },
				[3] = { X = 853, Y = 768 },
				[4] = { X = 605, Y = 768 },
				[5] = { X = 481, Y = 701 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				
				Points = {
					[1] = { X = 577, Y = 650 },
					[2] = { X = 631, Y = 666 },
					[3] = { X = 678, Y = 678 },
					[4] = { X = 770, Y = 703 },
					[5] = { X = 803, Y = 711 },
					[6] = { X = 844, Y = 712 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "RopeUp",
			
			X = 935,
			Y = 452
		},
		[2] = {
			Name = "RopeDown",
			
			X = 510,
			Y = 602
		}
	}	
}
