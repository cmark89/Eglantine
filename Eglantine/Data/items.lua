------------
--items.lua
------------

require "itemeffects.lua"



--Create a new table for items
items = {}


items["Grandfather's Book"] = {

	Name = "Grandfather's Book",
	Texture = "arrowicon"
	Description = "An old, dusty volume."
	Type = "Immediate"
	
	OnAcquire = function()
	end
	
	OnInspect = function()
		Event:ShowMessage(Description)
	end
	
	OnUse = function()
		print("Wow, this is a really boring book.")
	end	
}

