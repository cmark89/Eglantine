------------
--items.lua
------------

require("Data/Events/core_events")

-- Create the global rooms table
rooms = {}

-- List of all rooms to be loaded 
requiredRooms = { 
	"EmptyRoom",
	"SecretRoom"
}

-- Load each required room in turn
for key, value in pairs(requiredRooms) do
	require("Data/Rooms/"..value)
end
