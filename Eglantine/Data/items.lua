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
		Event:OpenPuzzlebox()
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

items["Letter"] = {
	
	Name = "Letter",
	--TEMP, MAKE ITS OWN GRAPHICS
	Texture = "Graphics/Objects/foldednote",
	Description = "A letter from one of Weather's associates.  He didn't seem to take it well...",
	Type = "Immediate",

	OnInspect = function()
		Event:ShowMessage("A letter from one of Weather's associates.  He didn't seem to take it well...")
	end,

	OnUse = function()
		Event:ViewDocument("Letter")
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

items["Folded Note"] = {

	Name = "Folded Note",
	Texture = "Graphics/Objects/foldednote",
	Description = "An old note that was sitting inside the puzzlebox.",
	Type = "Immediate",
	
	OnAcquire = nil,
	
	OnInspect = function()
		Event:ShowMessage("An old note that was sitting inside the puzzlebox.")
	end,
	
	OnUse = function()
		Event:ViewDocument("Folded Note")
	end
}

items["Strange Coin"] = {

	Name = "Strange Coin",
	Texture = "Graphics/Objects/coin",
	Description = "An eerie sigil is carved into this silver coin.  It feels...evil.",
	Type = "Unusable",
	
	OnAcquire = nil,
	
	OnInspect = function()
		Event:ShowMessage("An eerie sigil is carved into this silver coin.  It feels...evil.")
	end,
	
	OnUse = nil
}


items["Key"] = {

	Name = "Key",
	Texture = "Graphics/Objects/key",
	Description = "This key has a weird shape to it.  There's something wholly unsavory about it.",
	Type = "Active",
	
	OnAcquire = nil,
	
	OnInspect = function()
		Event:ShowMessage("This key has a weird shape to it.  There's something wholly unsavory about it.")
	end,
	
	OnUse = nil
}
