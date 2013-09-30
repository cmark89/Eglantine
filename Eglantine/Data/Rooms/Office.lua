print("Office added to global table 'rooms'.")

rooms["Office"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/Office",
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
				[1] = {X=0, Y=24},
				[2] = {X=131, Y=25},
				[3] = {X=130, Y=510},
				[4] = {X=0, Y=638}
			},
			
			Enabled = true,
			Drawn = false,
			
			InteractPoint = {
				X = 106,
				Y = 557
			},
			
			OnInteract = GameEvents.useOfficeDoor,
			OnLook = nil,
			Mouse = "Leave"
		},
		["I2"] = {
			Name = "CabinetMovementBlocker",
			Polygon = {
				[1] = { X = 266, Y = 493},
				[2] = { X = 324, Y = 410},
				[3] = { X = 385, Y = 411},
				[4] = { X = 366, Y = 496 }
			},
			Enabled = false,
			Drawn = false,
			InteractPoint = {
				X = 0,
				Y = 0
			},
			OnInteract = nil,
			OnLook=nil,
			BlocksMovement = true
		},
		["I3"] = {
			Name = "BottomDrawer_Open",
			Polygon = {
				[1] = {X=335, Y=368},
				[2] = {X=382, Y=328},
				[3] = {X=383, Y=406},
				[4] = {X=337, Y=465}
			},
			
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/OfficeBottomDrawer",
			DrawAt = { X = 262, Y = 318},
			
			InteractPoint = {
				X = 371,
				Y = 496
			},
			
			OnInteract = GameEvents.closeBottomDrawer,
			OnLook = nil,
			Mouse = "Hot"
		},
		["I4"] = {
			Name = "TopDrawer_Open",
			Polygon = {
				[1] = {X=316, Y=246},
				[2] = {X=363, Y=220},
				[3] = {X=364, Y=306},
				[4] = {X=316, Y=345}
			},
			
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/OfficeTopDrawer",
			DrawAt = { X = 259, Y = 209},
			
			InteractPoint = {
				X = 371,
				Y = 496
			},
			
			OnInteract = GameEvents.closeTopDrawer,
			OnLook = nil,
			Mouse = "Hot"
		},
		["I5"] = {
			Name = "TopDrawer_Closed",
			Polygon = {
				[1] = { X = 270, Y = 246 },
				[2] = { X = 316, Y = 221 },
				[3] = { X = 318, Y = 305 },
				[4] = { X = 270, Y = 344 }
			},
			
			Enabled = true,
			Drawn = false,
			
			InteractPoint = {
				X = 371,
				Y = 496
			},
			
			OnInteract = GameEvents.openTopDrawer,
			OnLook = nil,
			Mouse = "Hot"
		},
		["I6"] = {
			Name = "BottomDrawer_Closed",
			Polygon = {
				[1] = { X = 270, Y = 366},
				[2] = { X = 317, Y = 327},
				[3] = { X = 318, Y = 406},
				[4] = { X = 270, Y = 464},
			},
			
			Enabled = true,
			Drawn = false,
			
			InteractPoint = {
				X = 371,
				Y = 496
			},
			
			OnInteract = GameEvents.openBottomDrawer,
			OnLook = nil,
			Mouse = "Hot"
		},
		["I7"] = {
			Name = "Blueprints",
			Polygon = {
				[1] = { X = 282, Y = 252},
				[2] = { X = 315, Y = 235},
				[3] = { X = 315, Y = 241},
				[4] = { X = 311, Y = 245},
				[5] = { X = 310, Y = 272},
				[6] = { X = 283, Y = 272}
			},
			
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/DrawerBlueprints",
			DrawAt = {X=276,Y=230},
			
			InteractPoint = {
				X = 371,
				Y = 496
			},
			
			OnInteract = GameEvents.pickUpBlueprints,
			OnLook = nil,
			Mouse = "Grab"
		},
		["I8"] = {
			Name = "Strange Notes",
			Polygon = {
				[1] = { X = 291, Y = 256},
				[2] = { X = 310, Y = 246},
				[3] = { X = 310, Y = 272},
				[4] = { X = 293, Y = 271},
			},
			
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/DrawerNotes",
			DrawAt = {X=282,Y=241},
			
			InteractPoint = {
				X = 371,
				Y = 496
			},
			
			OnInteract = GameEvents.pickUpStrangeNotes,
			OnLook = nil,
			Mouse = "Grab"
		},
		["I9"] = {
			Name = "Photograph",
			Polygon = {
				[1] = { X = 281, Y = 382},
				[2] = { X = 298, Y = 364},
				[3] = { X = 305, Y = 366},
				[4] = { X = 305, Y = 394},
				[5] = { X = 281, Y = 394}
			},
			
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/DrawerPicture",
			DrawAt = {X=272,Y=359},
			
			InteractPoint = {
				X = 371,
				Y = 496
			},
			
			OnInteract = GameEvents.pickUpPhotograph,
			OnLook = nil,
			Mouse = "Grab"
		},
		["I10"] = {
			Name = "Journal",
			Polygon = {
				[1] = { X = 300, Y = 376 },
				[2] = { X = 322, Y = 358 },
				[3] = { X = 331, Y = 358 },
				[4] = { X = 330, Y = 393 },
				[5] = { X = 300, Y = 394 }
			},
			
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/DrawerJournal",
			DrawAt = {X=290,Y=350},
			
			InteractPoint = {
				X = 371,
				Y = 496
			},
			
			OnInteract = GameEvents.pickUpJournal,
			OnLook = nil,
			Mouse = "Grab"
		}
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 0, Y = 640 },
				[2] = { X = 152, Y = 498 },
				[3] = { X = 596, Y = 537 },
				[4] = { X = 597, Y = 768 }
			},
			[2] = {
				[1] = { X = 257, Y = 515 },
				[2] = { X = 322, Y = 414 },
				[3] = { X = 565, Y = 411 },
				[4] = { X = 579, Y = 540 }
			},
			[3] = {
				[1] = { X = 593, Y = 537 },
				[2] = { X = 963, Y = 535 },
				[3] = { X = 1024, Y = 649 },
				[4] = { X = 1024, Y = 768 },
				[5] = { X = 592, Y = 768 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = {
					[1] = { X = 278, Y = 512},
					[2] = { X = 365, Y = 519},
					[3] = { X = 444, Y = 525},
					[4] = { X = 509, Y = 531},
					[5] = { X = 564, Y = 536}
				}
			},
			[2] = {
				Connects = { 1, 3 },
				Points = {
					[1] = { X = 596, Y = 543},
					[2] = { X = 595, Y = 573},
					[3] = { X = 595, Y = 601},
					[4] = { X = 595, Y = 650},
					[5] = { X = 595, Y = 700},
					[6] = { X = 595, Y = 750}
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 105,
			Y = 556
		}
	},
	
	onLoad = GameEvents.startIndoorSounds,
	
	MinYValue = 768,
	MinYScale = 1,
	
	MaxYValue = 405,
	MaxYScale = .7
}