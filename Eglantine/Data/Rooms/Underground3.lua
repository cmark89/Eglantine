-- Load the room's events if need be
require("Data/Events/Underground3_events")

print("Underground3 added to global table 'rooms'.")

--Add an entry to the global "rooms" table
rooms["Underground3"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/underground3",
			Color = { 1, 1, 1, 1 },
			Type = "Background" --Midground, Foreground
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Rope",
			Area = {
				X = 17,
				Y = 0,
				Width = 84,
				Height = 515
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 118,
				Y = 518
			},

			OnInteract = function()
				door("Rope", "Underground2", "Rope")
			end,
			OnLook = function()
				Event:ShowMessage("I don't even know if I could climb that...")
			end
		},
		[2] = {
			Name = "Door",
			Polygon = {
				[1] = { X = 935, Y = 280 },
				[2] = { X = 1014, Y = 282 },
				[3] = { X = 1003, Y = 576 },
				[4] = { X = 913, Y = 462 }
			},
				
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 932,
				Y = 517
			},

			OnInteract = function()
				door("Door", "Underground4", "Door")
			end,
			OnLook = function()
				Event:ShowMessage("...")
			end
		}
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 98, Y = 477 },
				[2] = { X = 273, Y = 434 },
				[3] = { X = 469, Y = 426 },
				[4] = { X = 890, Y = 426 },
				[5] = { X = 998, Y = 581 },
				[6] = { X = 982, Y = 644 },
				[7] = { X = 847, Y = 663 },
				[8] = { X = 51, Y = 652 },
				[9] = { X = 19, Y = 617 },
				[10] = { X = 38, Y = 520 },
			}
		},
		
		Connections = {
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "Door",
			
			X = 932,
			Y = 515
		},
		[2] = {
			Name = "Rope",
			
			X = 114,
			Y = 520
		}
	}	
}
