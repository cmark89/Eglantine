luanet.load_assembly("Eglantine")
SCRIPTS = luanet.import_type("Eglantine.Engine.GameEvents")

scripts = {}

scripts["openingCutscene"] = SCRIPTS.openingCutscene
scripts["goodEnding"] = SCRIPTS.goodEnding
scripts["badEnding"] = SCRIPTS.badEnding