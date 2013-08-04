-- Load the room's events.
require("Data/Events/FrontYard_events")

print("FrontYard added to global table 'rooms'.")

rooms["FrontYard"] = {
------GRAPHICS------
	Layers = 
	{
		[1] = {
			Name = "BG",
			Texture = "Graphics/Rooms/FrontYard",
			Color = { 1, 1, 1, 1 },
			Scroll = { X = 0, Y = 0 },
			Type = "Background"
		}
	},
		
------OBJECTS AND EVENTS------
	Interactables = {
		[1] = {
			Name = "Door",
			Polygon = {
				[1] = { X = 256, Y = 187 },
				[2] = { X = 440, Y = 157 },
				[3] = { X = 470, Y = 465 },
				[4] = { X = 256, Y = 500 },
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 375,
				Y = 498
			},

			OnInteract = function()
				door("Door", "Foyer", "FrontDoor")
			end,
			OnLook = function()
				Event:ShowMessage("What a creepy looking place...")
			end
		},
		[2] = {
			Name = "BackYard",
			Area = {
				X = 900,
				Y = 0,
				Width = 126,
				Height = 355
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = true,
			Drawn = false,
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 976,
				Y = 375
			},

			OnInteract = function()
				door("BackYard", "BackYard", "Door")
			end,
			OnLook = nil
		},
		[3] = {
			Name = "Eglantine",
			Area = {
				X = 607,
				Y = 355,
				Width = 48,
				Height = 48
			},
				
			--Make it drawn later so it can be disabled...
			Enabled = false,
			Drawn = true,
			Texture = "Graphics/Objects/eglantine",
			
			-- This is where the player will path to in order to interact with the object
			InteractPoint = {
				X = 601,
				Y = 462
			},

			OnInteract = pickFrontYardEglantine,
			OnLook = function()
				Event:ShowMessage("Isn't that...?  From the picture?")
			end
		}
	},
	
	Triggers = {
	},
	
	--------NAVMESH--------
	Navmesh = {

		Polygons = {
			[1] = {
				[1] = { X = 0, Y = 538 },
				[2] = { X = 816, Y = 429 },
				[3] = { X = 1024, Y = 334 },
				[4] = { X = 1024, Y = 768 },
				[5] = { X = 0, Y = 768 }
			}
		},
		
		Connections = {
		}
	},
	
--------ENTRANCES--------
	Entrances = {
		[1] = {
			Name = "FrontDoor",
			
			X = 360,
			Y = 493
		},
		[2] = {
			Name = "BackYard",
			
			X = 976,
			Y = 375
		}
	},
	
	onEnter = checkForBloom
}