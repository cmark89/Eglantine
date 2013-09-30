print("Underground1 added to global table 'rooms'.")

--Add an entry to the global "rooms" table
rooms["Underground1"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/Underground1",
			Color = { 1, 1, 1, 1 },
			Type = "Background" --Midground, Foreground
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		["I1"] = {
			Name = "UpRope",
			Polygon = {
				[1] = {X = 62, Y = 0 },
				[2] = {X= 231, Y = 0 },
				[3] = {X = 241,Y = 413 },
				[4] = {X = 96,Y = 489 },
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 268,
				Y = 544
			},

			OnInteract = GameEvents.useUnderground1Door_Up,
			Mouse = "Leave"
		},
		["I2"] = {
			Name = "DownRope",
			Polygon = {
				[1] = { X = 713, Y = 618 },
				[2] = { X = 870, Y = 536 },
				[3] = { X = 978, Y = 546 },
				[4] = { X = 1001, Y = 578 },
				[5] = { X = 918, Y = 659 },
				[6] = { X = 775, Y = 685 },
				
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 792,
				Y = 560
			},

			OnInteract = GameEvents.useUnderground1Door_Down,
			Mouse = "Leave"
		}
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 0, Y = 690 },
				[2] = { X = 523, Y = 377 },
				[3] = { X = 988, Y = 464 },
				[4] = { X = 465, Y = 768 },
				[5] = { X = 0, Y = 768 }
			},
			[2] = {
				[1] = { X = 687, Y = 625 },
				[2] = { X = 878, Y = 768 },
				[3] = { X = 436, Y = 768 }
			},
			[3] = {
				[1] = { X = 791, Y = 714 },
				[2] = { X = 1024, Y = 601 },
				[3] = { X = 1024, Y = 768 },
				[4] = { X = 863, Y = 767 }
			}
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				
				Points = {
					[1] = { X = 464, Y = 754 },
					[2] = { X = 517, Y = 724 },
					[3] = { X = 557, Y = 701 },
					[4] = { X = 594, Y = 680 },
					[5] = { X = 628, Y = 661 },
					[6] = { X = 686, Y = 629 }
				}
			},
			[2] = {
				Connects = { 2, 3 },
				
				Points = {
					[1] = { X = 799, Y = 713 },
					[2] = { X = 829, Y = 736 },
					[3] = { X = 862, Y = 761 }
				}
			}
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "UpRope",
			
			X = 260,
			Y = 546
		},
		[2] = {
			Name = "DownRope",
			
			X = 806,
			Y = 561
		}
	}	
}