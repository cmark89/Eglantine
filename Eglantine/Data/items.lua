------------
--items.lua
------------

require "itemeffects.lua"



--Create a new table for items
items = {}


items["Grandfather's Book"] = {

	Name = "Grandfather's Book",
	Texture = "Graphics/Icons/GrandfathersBook"
	Type = "Immediate"
	
	OnAcquire = function()
	end
	
	OnUse = function()
		print("Wow, this is a really boring book.")
	end	
}

