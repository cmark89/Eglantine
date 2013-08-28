--Load the relevent assemblies and types
luanet.load_assembly "Eglantine"
EVENT_MANAGER = luanet.import_type "Eglantine.Engine.EventManager"

--Functions related to .NET classes and objects

--Sets the lua global Event to the current EventManager.Instance--
function loadEventManager()
	print("EventManager loaded.")
	Event = EVENT_MANAGER.Instance
end

loadEventManager()


menuPhase = 0


function showSplashScreen()
	runCoroutine(function()
		Event:PlaySong("Toxic Night", .7)
		waitSeconds(2)
		Event:MainMenuFadeIn("splash", 3)
		waitSeconds(7)
		Event:MainMenuFadeOut("splash", 3)
		waitSeconds(3)
		Event:NextMenuPhase()
	end)
end


function titleFadeIn()
	runCoroutine(function()
		Event:MainMenuHideElement("splash")
		
		Event:MainMenuFadeIn("background", 5);
		waitSeconds(6)
		Event:MainMenuFadeIn("title", 3);
		waitSeconds(3)
		Event:NextMenuPhase()
	end)
end


function mainMenu()
	Event:MainMenuFadeIn("background", 0);
	Event:MainMenuFadeIn("title", 0);

	Event:ShowMainMenu()
end

function onStartNewGame()
	runCoroutine(function()
		Event:PlaySound("windowbreak")
		Event:FadeMusic(0,0)
		Event:LockMenuInput()
		waitSeconds(2.5)
		Event:MainMenuFadeOut("background", 3)
		Event:MainMenuFadeOut("title", 3)
		waitSeconds(3.5)
		Event:PlayStorySequence("openingCutscene")
	end)
end

menuEvents = {
	[1] = showSplashScreen,
	[2] = titleFadeIn,
	[3] = mainMenu
}