require "Data/rooms"
require "Data/items"
require "Data/documents"

--Load the relevent assemblies and types
luanet.load_assembly "Eglantine"

ItemEvents = luanet.import_type "Eglantine.Engine.ItemEvents"
GameEvents = luanet.import_type "Eglantine.Engine.GameEvents"
