#!/usr/local/bin/lua
------------
--items.lua
------------

--require "mainevents.lua"???

--Test making a navmesh
rooms = {}

rooms["testroom"] = {
--------GRAPHICS--------
	BackgroundTextures = "Content/somebgfile",
	ForegroundTextures = { "Someotherfile", "Andanotherfordepth" },
	
	
--------OBJECTS AND EVENTS--------
	Interactables = {
		[1] = {
			X = 6,
			Y = 55,
			Width = 17,
			Height = 22,
			
			OnInteract = someFunction
		}
	},
	
	Triggers = {
		[1] = {
			X = 67,
			Y = 550,
			Width = 170,
			Height = 212,
			
			OnEnter = someOtherFunction
		}
	},	
--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 1, Y = 753 },
				[2] = { X = 0, Y = 768 },
				[3] = { X = 330, Y = 728 },
				[4] = { X = 364, Y = 765 }
			},
			
			[2] = {
				[1] = { X = 354, Y = 768 },
				[2] = { X = 323, Y = 728 },
				[3] = { X = 322, Y = 623 },
				[4] = { X = 458, Y = 566 },
				[5] = { X = 461, Y = 765 }
			},
			
			[3] = {
				[1] = { X = 455, Y = 765 },
				[2] = { X = 454, Y = 572 },
				[3] = { X = 862, Y = 543 },
				[4] = { X = 1023, Y = 590 },
				[5] = { X = 1021, Y = 766 }
			},
			
			[4] = {
				[1] = { X = 360, Y = 552 },
				[2] = { X = 420, Y = 550 },
				[3] = { X = 456, Y = 571 },
				[4] = { X = 361, Y = 617 }
			},
			
			[5] = {
				[1] = { X = 737, Y = 530 },
				[2] = { X = 836, Y = 521 },
				[3] = { X = 741, Y = 555 },
				[4] = { X = 863, Y = 549 }
			},
			
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = {
					[1] = { X = 328, Y = 729 },
					[2] = { X = 343, Y = 749 },
					[3] = { X = 357, Y = 764 }
				}
			},
			
			[2] = {
				Connects = { 2, 3 },
				Points = {
					[1] = { X = 457, Y = 574 },
					[2] = { X = 457, Y = 662 },
					[3] = { X = 458, Y = 760 }
				}
			},
			
			[3] = {
				Connects = { 2, 4 },
				Points = {
					[1] = { X = 361, Y = 611 },
					[2] = { X = 457, Y = 662 },
					[3] = { X = 404, Y = 591 }
				}
			},
			
			[4] = {
				Connects = { 3, 5 },
				Points = {
					[1] = { X = 744, Y = 553 },
					[2] = { X = 800, Y = 549 },
					[3] = { X = 857, Y = 546 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = 
	{
		[1] = {
			X = 67,
			Y = 550,
			Width = 170,
			Height = 212,
			
			Destination = "testroom2"
		}
	}
}
