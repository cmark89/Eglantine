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

items["Scissors"] = {

	Name = "Scissors",
	Texture = "Graphics/Objects/scissors",
	Description = "They seem pretty dull, but I bet they could still cut.",
	Type = "Active",
	
	OnInspect = function()
		Event:ShowMessage("They seem pretty dull, but I bet they could still cut.")
	end,
	
	OnUse = nil
}

items["Crowbar"] = {

	Name = "Crowbar",
	Texture = "Graphics/Objects/crowbar",
	Description = "Rusty, but it's still metal.  Could probably pry something open with it.",
	Type = "Active",
	
	OnInspect = function()
		Event:ShowMessage("Rusty, but it's still metal.  Could probably pry something open with it.")
	end,
	
	OnUse = nil
}

items["Eglantine"] = {
	Name = "Eglantine",
	Texture = "Graphics/Objects/eglantine",
	Descriptin = "Even if it's pretty, there's something dark about this particular flower...",
	Type = "Unusable",
	
	OnInspect = function()
		Event:ShowMessage("Even if it's pretty, there's something dark about this particular flower...")
	end,
	
	OnUse = nil --For now
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

items["Strange Notes"] = {

	Name = "Strange Notes",
	Texture = "Graphics/Objects/document1",
	Description = "A set of strange, crazed writings.",
	Type = "Immediate",
	
	OnInspect = function()
		Event:ShowMessage("A set of strange, crazed writings.")
	end,
	
	OnUse = function()
		Event:ViewDocument("Strange Notes")
	end
}

items["Journal"] = {

	Name = "Journal",
	Texture = "Graphics/Objects/book",
	Description = "It looks like a personal diary of old man Weathers.",
	Type = "Immediate",
	
	OnInspect = function()
		Event:ShowMessage("It looks like a personal diary of old man Weathers.")
	end,
	
	OnUse = function()
		Event:ViewDocument("Journal")
	end
}

items["Blueprints"] = {

	Name = "Blueprints",
	Texture = "Graphics/Objects/blueprints",
	Description = "These appear to be the blueprints to the building...",
	Type = "Immediate",
	
	OnInspect = function()
		Event:ShowMessage("These appear to be the blueprints to the building...")
	end,
	
	OnUse = function()
		Event:ViewDocument("Blueprints")
	end
}

items["Photograph"] = {

	Name = "Photograph",
	Texture = "Graphics/Objects/photo",
	Description = "This photograph...",
	Type = "Immediate",
	
	OnAcquire = function()
		GameState.PhotoTaken = true
	end,
	
	OnInspect = function()
		Event:ShowMessage("This photograph...")
	end,
	
	OnUse = function()
		Event:ViewDocument("Photograph")
	end
}

