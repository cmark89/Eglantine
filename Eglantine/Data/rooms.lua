------------
--items.lua
------------

require("Data/Events/core_events")

-- Create the global rooms table
rooms = {}

-- List of all rooms to be loaded 
requiredRooms = { 
	"FrontYard",
	"BackYard",
	"Foyer",
	"Kitchen",
	"Upstairs",
	"Bedroom",
	"Bathroom",
	"Office",
	"EmptyRoom",
	"SecretRoom",
	"LivingRoom",
	"Underground1",
	"Underground2",
	"Underground3",
	"Underground4"
}

-- Load each required room in turn
for key, value in pairs(requiredRooms) do
	require("Data/Rooms/"..value)
end
