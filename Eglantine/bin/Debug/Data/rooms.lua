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
				[1] = { X = 1, Y = 20 },
				[2] = { X = 5, Y = 3 },
				[3] = { X = 9, Y = 12 },
				[4] = { X = 15, Y = 12 },
			},
			
			[2] = {
				[1] = { X = 11, Y = 202 },
				[2] = { X = 15, Y = 32 },
				[3] = { X = 19, Y = 122 },
				[4] = { X = 115, Y = 122 },
			}	
		},
		
		Connections = {
			[1] = {
				Connects = { 1, 2 },
				Points = {
					[1] = { X = 50, Y = 700 },
					[2] = { X = 60, Y = 760 },
					[3] = { X = 150, Y = 720 }
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
