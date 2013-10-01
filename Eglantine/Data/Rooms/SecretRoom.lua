print("SecretRoom added to global table 'rooms'.")

rooms["SecretRoom"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/SecretRoom",
			Color = { 1, 1, 1, 1 },
			Scroll = { X = 0, Y = 0 },
			Type = "Background"
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		["I1"] = {
			Name = "Puzzlebox",
			Area = {
				X = 433,
				Y = 265,
				Width = 48,
				Height = 48
			},
				
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/puzzlebox",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 474,
				Y = 407
			},

			OnInteract = GameEvents.pickUpPuzzlebox,
			OnLook = GameEvents.lookAtPuzzlebox,
			Mouse = "Grab"
		},
		
		["I2"] = {
			Name = "Door",
			Polygon = {
				[1] = { X = 650, Y = 82 },
				[2] = { X = 794, Y = 96 },
				[3] = { X = 794, Y = 377 },
				[4] = { X = 648, Y = 345 },
				
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 698,
				Y = 437
			},

			OnInteract = GameEvents.useSecretRoomDoor_Painting,
			OnLook = GameEvents.lookAtPainting,
			Mouse = "Leave"
		},
		
		["I3"] = {
			Name = "TrapdoorClosed",
			Polygon = {
				[1] = { X = 44, Y = 568 },
				[2] = { X = 279, Y = 525 },
				[3] = { X = 470, Y = 600 },
				[4] = { X = 227, Y = 657 }
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 363,
				Y = 645
			},

			OnInteract = GameEvents.interactWithTrapdoor,
			OnLook = GameEvents.lookAtTrapdoor,
			Mouse = "Hot"
		},
		["I4"] = {
			Name = "TrapdoorOpenGraphic",
			Area = {
				X = 0,
				Y = 311,
				Width = 0,
				Height = 0
			},
			YCutoff = 568,
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/trapdooropen",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 363,
				Y = 645
			},

			OnInteract = nil,
			OnLook = nil
		},
		["I5"] = {
			Name = "TrapdoorOpen",
			Polygon = {
				[1] = { X = 44, Y = 568 },
				[2] = { X = 279, Y = 525 },
				[3] = { X = 470, Y = 600 },
				[4] = { X = 227, Y = 657 }
			},
			
			Enabled = false,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 363,
				Y = 645
			},

			OnInteract = GameEvents.useSecretRoomDoor_TrapDoor,
			OnLook = GameEvents.lookDownTrapdoor,
			
			BlocksMovement = true,
			Mouse = "Leave"
		},
		["I6"] = {
			Name = "TrapdoorHatch_Open",
			Polygon = {
				[1] = { X = 0, Y = 348 },
				[2] = { X = 193, Y = 325 },
				[3] = { X = 278, Y = 526 },
				[4] = { X = 46, Y = 570 },
				[5] = { X = 0, Y = 487 }
			},
			
			Enabled = false,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 363,
				Y = 645
			},

			OnInteract = GameEvents.closeTrapdoor,
			OnLook = nil,
			
			BlocksMovement = true,
			Mouse = "Hot"
		}
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			--Top left
			[1] = {
				[1] = { X = 0, Y = 414 },
				[2] = { X = 270, Y = 383 },
				[3] = { X = 564, Y = 474 },
				[4] = { X = 0, Y = 578 },
			},
			--Right side
			[2] = {
				[1] = { X = 355, Y = 418 },
				[2] = { X = 553, Y = 389 },
				[3] = { X = 1024, Y = 501 },
				[4] = { X = 1024, Y = 768 },
				[5] = { X = 864, Y = 768 },
				[6] = { X = 253, Y = 510 },
				[7] = { X = 506, Y = 470 }
			},
			--Bottom
			[3] = {
				[1] = { X = 0, Y = 713 },
				[2] = { X = 486, Y = 596 },
				[3] = { X = 904, Y = 768 },
				[4] = { X = 0, Y = 768},
			},
			--Left
			[4] = {
				[1] = { X = 0, Y = 560 },
				[2] = { X = 28, Y = 561 },
				[3] = { X = 244, Y = 666 },
				[4] = { X = 0, Y = 724},
			}
			
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = { 
					[1] = { X = 292, Y = 420 },
					[2] = { X = 312, Y = 509 },
					[3] = { X = 339, Y = 505 },
					[4] = { X = 371, Y = 501 },
					[5] = { X = 412, Y = 492 },
					[6] = { X = 439, Y = 487 },
					[7] = { X = 478, Y = 481 },
					[8] = { X = 506, Y = 476 },
					[9] = { X = 511, Y = 462 },
					[10] = { X = 477, Y = 451 },
					[11] = { X = 453, Y = 442 },
					[12] = { X = 424, Y = 434 },
					[13] = { X = 402, Y = 429 },
					[13] = { X = 376, Y = 421 }
				}
			},
			[2] = {
				Connects = { 1, 4 },
				Points = { 
					[1] = { X = 3, Y = 570 },
					[2] = { X = 15, Y = 568 },
					[3] = { X = 26, Y = 567 }
				}
			},
			[3] = {
				Connects = { 3, 4 },
				Points = { 
					[1] = { X = 6, Y = 716 },
					[2] = { X = 25, Y = 714 },
					[3] = { X = 54, Y = 706 },
					[4] = { X = 88, Y = 698 },
					[5] = { X = 128, Y = 689 },
					[6] = { X = 158, Y = 680 },
					[7] = { X = 200, Y = 671 },
					[8] = { X = 225, Y = 664 }
				}
			},
			[4] = {
				Connects = { 2, 3 },
				Points = { 
					[1] = { X = 486, Y = 600 },
					[2] = { X = 507, Y = 610 },
					[3] = { X = 530, Y = 619 },
					[4] = { X = 547, Y = 627 },
					[5] = { X = 567, Y = 636 },
					[6] = { X = 593, Y = 646 },
					[7] = { X = 622, Y = 657 },
					[8] = { X = 647, Y = 668 },
					[9] = { X = 686, Y = 684 },
					[10] = { X = 718, Y = 696 },
					[11] = { X = 753, Y = 710 },
					[12] = { X = 787, Y = 725 },
					[13] = { X = 829, Y = 740 },
					[14] = { X = 864, Y = 755 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 689,
			Y = 444
		},
		
		[2] = {
			Name = "Trapdoor",
			
			X = 362,
			Y = 648
		}
	},
	
	onEnter = GameEvents.checkTVStatic,
	onExit = GameEvents.leaveSecretRoom,
	onLoad = GameEvents.startIndoorSounds,
	
	MinYValue = 768,
	MinYScale = .8,
	
	MaxYValue = 385,
	MaxYScale = .6,
	
	defaultFootprintSound = "footstepStone"
}