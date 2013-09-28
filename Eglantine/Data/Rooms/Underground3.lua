print("Underground3 added to global table 'rooms'.")

--Add an entry to the global "rooms" table
rooms["Underground3"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/Underground3",
			Color = { 1, 1, 1, 1 },
			Type = "Background" --Midground, Foreground
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "RopeUp",
			Polygon = {
				[1] = { X = 45, Y = 0 },
				[2] = { X = 217, Y = 0 },
				[3] = { X = 231, Y = 459 },
				[4] = { X = 101, Y = 478 },
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 171,
				Y = 479
			},

			OnInteract = GameEvents.useUnderground3Door_Up,
			Mouse = "Leave"
		},
		[2] = {
			Name = "Door",
			Polygon = {
				[1] = { X = 751, Y = 77 },
				[2] = { X = 1014, Y = 74 },
				[3] = { X = 1024, Y = 597 },
				[4] = { X = 749, Y = 562 }
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 877,
				Y = 607
			},

			OnInteract = GameEvents.useUnderground3Door_Down,
			Mouse = "Leave"
		}
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 107, Y = 485 },
				[2] = { X = 362, Y = 459 },
				[3] = { X = 382, Y = 757 },
				[4] = { X = 101, Y = 761 }
			},
			[2] = {
				[1] = { X = 351, Y = 463 },
				[2] = { X = 453, Y = 491 },
				[3] = { X = 501, Y = 592 },
				[4] = { X = 365, Y = 599 }
			},
			[3] = {
				[1] = { X = 446, Y = 485 },
				[2] = { X = 1024, Y = 600 },
				[3] = { X = 1024, Y = 768 },
				[4] = { X = 834, Y = 761 },
				[5] = { X = 496, Y = 600 }
			},
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = {
					[1] = { X = 359, Y = 481},
					[2] = { X = 361, Y = 518},
					[3] = { X = 364, Y = 552},
					[4] = { X = 367, Y = 588}
				}
			},
			[2] = {
				Connects = { 2, 3 },
				Points = {
					[1] = { X = 454, Y = 497},
					[2] = { X = 462, Y = 521},
					[3] = { X = 474, Y = 543},
					[4] = { X = 485, Y = 565},
					[5] = { X = 492, Y = 583}
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "RopeUp",
			
			X = 171,
			Y = 479
		},
		[2] = {
			Name = "Door",
			
			X = 866,
			Y = 614
		}
	}	
}
