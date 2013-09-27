-- Load the room's events if need be
require("Data/Events/EmptyRoom_events")

print("EmptyRoom added to global table 'rooms'.")

--Add an entry to the global "rooms" table
rooms["EmptyRoom"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/EmptyRoom",
			Color = { 1, 1, 1, 1 },
			Type = "Background" 
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Door",
			
			Polygon = {
				[1] = { X = 0, Y = 67 },
				[2] = { X = 67, Y = 45 },
				[3] = { X = 70, Y = 523 },
				[4] = { X = 0, Y = 657 }
			},
			
			Enabled = true,
			Drawn = false,
			
			InteractPoint = {
				X = 48,
				Y = 588
			},
			
			OnInteract = GameEvents.useEmptyRoomDoor,
			Mouse = "Leave"
		},
		[2] = {
			Name = "Puzzle Key",
			
			Area = {
				X = 144,
				Y = 408,
				Height = 48,
				Width = 48 
			},
			
			Enabled = true,
			Drawn = true,
			Texture = "Graphics/Objects/puzzlekey",
			
			InteractPoint = {
				X = 178,
				Y = 445
			},
			
			OnInteract = GameEvents.pickUpPuzzleKey,
			OnLook = GameEvents.lookAtPuzzleKey,
			Mouse = "Grab"
		}
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 0, Y = 768 },
				[2] = { X = 0, Y = 650 },
				[3] = { X = 135, Y = 405 },
				[4] = { X = 883, Y = 405 },
				[5] = { X = 1024, Y = 650 },
				[6] = { X = 1024, Y = 768 },
			}
		},
		
		Connections = {
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 81,
			Y = 543
		}
	}	
}