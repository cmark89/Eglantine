------------
--items.lua
------------

-- Create the global rooms table
rooms = {}

-- List of all rooms to be loaded 
requiredRooms = { "testroom" }
print("Created global table \"testroom\"")

-- Load each required room in turn
for key, value in pairs(requiredRooms) do
	require("Data/Rooms/"..value)
end
