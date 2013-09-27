------------
--items.lua
------------
ItemEvents = luanet.import_type "Eglantine.Engine.ItemEvents"

--Create a new table for items
items = {}

items["Scissors"] = {

	Name = "Scissors",
	Texture = "Graphics/Objects/scissors",
	Description = "They seem pretty dull, but I bet they could still cut.",
	Type = "Active"
}

items["Crowbar"] = {

	Name = "Crowbar",
	Texture = "Graphics/Objects/crowbar",
	Description = "Rusty, but it's still metal.  Could probably pry something open with it.",
	Type = "Active"
}

items["Eglantine"] = {
	Name = "Eglantine",
	Texture = "Graphics/Objects/eglantine",
	Descriptin = "Even if it's pretty, there's something dark about this particular flower...",
	Type = "Unusable"
}

items["Puzzlebox"] = {

	Name = "Puzzlebox",
	Texture = "Graphics/Objects/puzzlebox",
	Description = "A strange puzzlebox.  I should try to solve it!",
	Type = "Immediate",
	
	OnUse = ItemEvents.usePuzzlebox;
}

items["Strange Notes"] = {

	Name = "Strange Notes",
	Texture = "Graphics/Objects/document1",
	Description = "A set of strange, crazed writings.",
	Type = "Immediate",
	
	OnUse = ItemEvents.useStrangeNotes;
}

items["Journal"] = {

	Name = "Journal",
	Texture = "Graphics/Objects/book",
	Description = "It looks like a personal diary of old man Weathers.",
	Type = "Immediate",
	
	OnInspect = function()
		Event:ShowMessage("It looks like a personal diary of old man Weathers.")
	end,
	
	OnUse = ItemEvents.useJournal;
}

items["Blueprints"] = {

	Name = "Blueprints",
	Texture = "Graphics/Objects/blueprints",
	Description = "These appear to be the blueprints to the building...",
	Type = "Immediate",
	
	OnUse = ItemEvents.useBlueprints;
}

items["Letter"] = {
	
	Name = "Letter",
	--TEMP, MAKE ITS OWN GRAPHICS
	Texture = "Graphics/Objects/foldednote",
	Description = "A letter from one of Weather's associates.  He didn't seem to take it well...",
	Type = "Immediate",

	OnUse = ItemEvents.useLetter;
}

items["Photograph"] = {

	Name = "Photograph",
	Texture = "Graphics/Objects/photo",
	Description = "This photograph...",
	Type = "Immediate",
	
	OnAcquire = ItemEvents.acquirePhotograph;
	OnUse = ItemEvents.usePhotograph;
}

items["Folded Note"] = {

	Name = "Folded Note",
	Texture = "Graphics/Objects/foldednote",
	Description = "An old note that was sitting inside the puzzlebox.",
	Type = "Immediate",
	
	OnUse = ItemEvents.useFoldedNote;
}

items["Strange Coin"] = {

	Name = "Strange Coin",
	Texture = "Graphics/Objects/coin",
	Description = "An eerie sigil is carved into this silver coin.  It feels...evil.",
	Type = "Unusable",
}


items["Key"] = {

	Name = "Key",
	Texture = "Graphics/Objects/key",
	Description = "This key has a weird shape to it.  There's something wholly unsavory about it.",
	Type = "Active",
}


items["Puzzle Key"] = {

	Name = "Puzzle Key",
	Texture = "Graphics/Objects/puzzlekey",
	Description = "There's a notch cut out on the head of this thing.  It looks like it's supposed to screw into a hole somewhere.",
	Type = "Immediate",
	
	OnUse = ItemEvents.usePuzzleKey;	
}
