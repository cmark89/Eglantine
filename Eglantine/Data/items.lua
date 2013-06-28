------------
--items.lua
------------

require "Data/itemeffects"



--Create a new table for items
items = {}


items["Grandfather's Book"] = {

	Name = "Grandfather's Book",
	Texture = "arrowicon",
	Description = "An old, dusty volume.",
	Type = "Immediate",
	
	OnAcquire = nil,
	
	OnInspect = function()
		Event:ShowMessage(Description)
	end,
	
	OnUse = function()
		print("Wow, this is a really boring book.")
	end	
}

items["Screwdriver"] = {

	Name = "Screwdriver",
	Texture = "arrowicon",
	Description = "I could probably fix an outlet with this!",
	Type = "Active",
	
	OnInspect = function()
		Event:ShowMessage("I could probably fix an outlet with this!")
	end,
	
	OnUse = nil
}

items["Puzzlebox"] = {

	Name = "Puzzlebox",
	Texture = "Graphics/Objects/puzzlebox",
	Description = "A strange puzzlebox.  I should try to solve it!",
	Type = "Immediate",
	
	OnInspect = function()
		Event:ShowMessage("A strange puzzlebox.  I should try to solve it!")
	end,
	
	OnUse = function()
		Event:ShowMessage("THE PUZZLE SCREEN OPENS NOW!")
	end
}

