print("LivingRoom added to global table 'rooms'.")

rooms["LivingRoom"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/LivingRoom",
			Color = { 1, 1, 1, 1 },
			Scroll = { X = 0, Y = 0 },
			Type = "Background"
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		["I1"] = {
			Name = "Painting",
			Polygon = {
				[1] = { X = 230, Y = 99 },
				[2] = { X = 369, Y = 83 },
				[3] = { X = 375, Y = 345 },
				[4] = { X = 231, Y = 372 },
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 324,
				Y = 424
			},

			OnInteract = GameEvents.interactWithPainting,
			OnLook = GameEvents.lookAtPainting,
			Mouse = "Hot"
		},
		["I2"] = {
			Name = "PaintingDoor",
			Polygon = {
				[1] = { X = 230, Y = 99 },
				[2] = { X = 369, Y = 83 },
				[3] = { X = 375, Y = 345 },
				[4] = { X = 231, Y = 372 },
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = false,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 324,
				Y = 424
			},

			OnInteract = GameEvents.useLivingRoomDoor_Painting,
			OnLook = GameEvents.lookAtPainting,
			Mouse = "Leave"
		},
		
		["I3"] = {
			Name = "Door",
			Polygon = {
				[1] = { X = 825, Y = 69 },
				[2] = { X = 976, Y = 73 },
				[3] = { X = 980, Y = 406 },
				[4] = { X = 817, Y = 387 },
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 895,
				Y = 408
			},

			OnInteract = GameEvents.useLivingRoomDoor_Foyer,
			Mouse = "Leave"
		},
		["I4"] = {
			Name = "TV",
			Polygon = {
				[1] = { X = 600, Y = 226 },
				[2] = { X = 712, Y = 236 },
				[3] = { X = 717, Y = 306 },
				[4] = { X = 690, Y = 316 },
				[5] = { X = 597, Y = 303 }
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 627,
				Y = 414
			},

			OnInteract = GameEvents.interactWithTV,
			OnLook = GameEvents.lookAtTV,
			Mouse = "Hot"
		},
		["I5"] = {
			Name = "Static1",
			Area = {
					X = 604,
					Y = 239,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/static1",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			}
		},
		["I6"] = {
			Name = "Static2",
			Area = {
					X = 604,
					Y = 239,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/static2",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			}
		},
		["I7"] = {
			Name = "Static3",
			Area = {
					X = 604,
					Y = 239,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/static3",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			}
		},
		["I8"] = {
			Name = "Static4",
			Area = {
					X = 604,
					Y = 239,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/static4",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			}
		},
		["I9"] = {
			Name = "Static5",
			Area = {
					X = 604,
					Y = 239,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/static5",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			}
		},
		["I10"] = {
			Name = "Static6",
			Area = {
					X = 604,
					Y = 239,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/static6",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			}
		},
		["I11"] = {
			Name = "Tear",
			Area = {
					X = 248,
					Y = 99,
					Width = 0,
					Height = 0
			},
				
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/LivingRoomTear",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 0,
				Y = 0
			},
			Mouse = "Leave"
		}
	},
	
	Triggers = {
		["I1"] = {
			Name = "TVActivate",
			Area = {
				X = 452,
				Y = 366,
				Width = 51,
				Height = 422
			},
			Enabled = true,
		
			OnEnter = GameEvents.turnOnTV
		}
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 0, Y = 498 },
				[2] = { X = 475, Y = 384 },
				[3] = { X = 738, Y = 432 },
				[4] = { X = 204, Y = 768 },
				[5] = { X = 0, Y = 768 }
			},
			[2] = {
				[1] = { X = 782, Y = 387 },
				[2] = { X = 1024, Y = 414 },
				[3] = { X = 1024, Y = 768 },
				[4] = { X = 160, Y = 768 },
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = { 
					[1] = { X = 194, Y = 750 },
					[2] = { X = 220, Y = 733 },
					[3] = { X = 270, Y = 703 },
					[4] = { X = 321, Y = 675 },
					[5] = { X = 384, Y = 641 },
					[6] = { X = 426, Y = 610 },
					[7] = { X = 478, Y = 576 },
					[8] = { X = 526, Y = 553 },
					[9] = { X = 572, Y = 520 },
					[10] = { X = 614, Y = 500 },
					[11] = { X = 656, Y = 472 },
					[12] = { X = 690, Y = 447 },
					[13] = { X = 717, Y = 434 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 892,
			Y = 409
		},
		
		[2] = {
			Name = "Painting",
			
			X = 316,
			Y = 427
		}
	},
	
	onEnter = GameEvents.enterLivingRoom,
	onExit = GameEvents.leaveLivingRoom,
	onLoad = GameEvents.loadLivingRoom,
	
	MinYValue = 768,
	MinYScale = .75,
	
	MaxYValue = 364,
	MaxYScale = .6,
	
	defaultFootprintSound = "footstepWood"
}