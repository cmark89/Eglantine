print("Underground4 added to global table 'rooms'.")

--Add an entry to the global "rooms" table
rooms["Underground4"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/Underground4",
			Color = { 1, 1, 1, 1 },
			Type = "Background" --Midground, Foreground
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Door",
			Polygon = {
				[1] = { X = 0, Y = 223 },
				[2] = { X = 19, Y = 198 },
				[3] = { X = 38, Y = 206 },
				[4] = { X = 95, Y = 304 },
				[5] = { X = 114, Y = 413 },
				[6] = { X = 113, Y = 530 },
				[7] = { X = 2, Y = 612 },
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 118,
				Y = 518
			},

			OnInteract = GameEvents.useUnderground4Door,
			OnLook = nil,
			Mouse = "Hot"
		},
		
		[2] = {
			Name = "Artifact",
			Area = {
				X = 411,
				Y = 201,
				Width = 200,
				Height = 500
			},
				
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/artifact",
			YCutoff = 593,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[3] = {
			Name = "Flower1",
			Area = {
				X = 459,
				Y = 660,
				Width = 48,
				Height = 48
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/KitchenFlower",
			YCutoff = 611,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[4] = {
			Name = "Flower2",
			Area = {
				X = 432,
				Y = 666,
				Width = 48,
				Height = 48
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/KitchenFlower",
			YCutoff = 611,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[5] = {
			Name = "Flower3",
			Area = {
				X = 475,
				Y = 681,
				Width = 48,
				Height = 48
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/KitchenFlower",
			YCutoff = 611,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},

			OnInteract = nil,
			OnLook = nil
		},
		[6] = {
			Name = "GraveMovementBlocker",
			Polygon = {
				[1] = { X = 426, Y = 629 },
				[2] = { X = 465, Y = 602 },
				[3] = { X = 575, Y = 637 },
				[4] = { X = 576, Y = 652 },
				[5] = { X = 536, Y = 686 },
				[6] = { X = 425, Y = 652 }
			},
			Enabled = true,
			Drawn = false,
			InteractPoint = {
				X = 0,
				Y = 0,
			},
			OnInteract = nil,
			OnLook = nil,
			BlocksMovement = true
		},
		[7] = {
			Name = "Grave",
			Area = {
				X = 398,
				Y = 480,
				Width = 210,
				Height = 220
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/grave",
			YCutoff = 612,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 446,
				Y = 692
			},

			OnInteract = GameEvents.interactWithGrave,
			OnLook = GameEvents.readHeadstone,
			Mouse = "Hot"
		},
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 0, Y = 619 },
				[2] = { X = 122, Y = 548 },
				[3] = { X = 290, Y = 547 },
				[4] = { X = 878, Y = 612 },
				[5] = { X = 743, Y = 768 },
				[6] = { X = 0, Y = 768 }
			},
			[2] = {
				[1] = { X = 430, Y = 435 },
				[2] = { X = 637, Y = 385 },
				[3] = { X = 903, Y = 511 },
				[4] = { X = 845, Y = 623 },
				[5] = { X = 301, Y = 567 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = {1, 2},
				
				Points = {
					[1] = { X = 326, Y = 561 },
					[2] = { X = 392, Y = 567 },
					[3] = { X = 443, Y = 573 },
					[4] = { X = 486, Y = 578 },
					[5] = { X = 532, Y = 582 },
					[6] = { X = 578, Y = 587 },
					[7] = { X = 611, Y = 592 },
					[8] = { X = 653, Y = 596 },
					[9] = { X = 701, Y = 600 },
					[10] = { X = 735, Y = 604 },
					[11] = { X = 779, Y = 607 },
					[12] = { X = 833, Y = 614 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 57,
			Y = 602
		}
	},
	
	onEnter = GameEvents.beginEnding
}

