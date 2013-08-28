--This is redundant

--Load the relevent assemblies and types
luanet.load_assembly "Eglantine"
GAME_STATE = luanet.import_type "Eglantine.Engine.GameState"
EVENT_MANAGER = luanet.import_type "Eglantine.Engine.EventManager"

--Functions related to .NET classes and objects
--Sets the lua global GameState to the current GameState.Instance--
function loadGameState()
	print("GameState loaded.")
	GameState = GAME_STATE.Instance
end

--Sets the lua global Event to the current EventManager.Instance--
function loadEventManager()
	print("EventManager loaded.")
	Event = EVENT_MANAGER.Instance
end

loadEventManager()


function openingCutscene()
	
	runCoroutine(function()
		Event:PlaySong("Bloom, Discord")
		waitSeconds(2)
		Event:ShowStoryMessage("This is a story from my childhood.")
		Event:ShowStoryMessage("It's a story that lies somewhere between dream and reality.  To be honest, I can no longer remember which category to assign it to . . . ")
		waitUntil("Messages read")
		
		Event:SetStoryImage("Graphics/Cutscenes/opening1")
		Event:LerpStoryColor(1,1,1,1,2)
		waitSeconds(2)
		
		Event:ShowStoryMessage("I spent a lot of time with my grandparents in Widow's Creek when I was young.")
		Event:ShowStoryMessage("I have fond memories of laying in the grass, staring at the unpolluted stars.  It was there that I fell in love with stargazing.")
		Event:ShowStoryMessage("In Widow's Creek, the moon always rose behind a particular house on a hill.")
		Event:ShowStoryMessage("I didn't know it at the time, but that house belonged to an old man named Edward Weathers.")
		waitUntil("Messages read")
		waitSeconds(1)
		Event:LerpStoryColor(0,0,0,0,2)
		Event:ShowStoryMessage("When I got a little older, I asked my grandparents who lived in that house.")
		waitSeconds(2)
		Event:ShowStoryMessage("They told me that it was abandoned.  Old Mr. Weathers had apparently died under bizarre circumstances not long before.")
		Event:ShowStoryMessage("It took a while for that to sink in.  I hadn't known him, but his death disturbed me.")
		Event:ShowStoryMessage("That's because sometimes, as evening turned into night, lights would go on and off in that abandoned house on the hill.")
		Event:SetStoryImage("Graphics/Cutscenes/opening3")
		Event:LerpStoryColor(1,1,1,1,4)
		waitUntil("Messages read")
		
		Event:LerpStoryColor(0,0,0,0,5)
		Event:ShowStoryMessage("Had I been a little older, I would have just assumed it was vandals and teenagers treading on the old man's legacy.  Of course, I would have been wrong.")
		
		waitSeconds(2)
		Event:ShowStoryMessage("It was during the summer of my eleventh birthday.  I remember that distinctly, because it was the last summer I would spend in Widow's Creek before both my grandparents died.")
		waitUntil("Messages read")
		
		Event:SetStoryImage("Graphics/Cutscenes/opening4")
		Event:LerpStoryColor(1,1,1,1,3)
		Event:ShowStoryMessage("I decided that I was going to explore the house.  I was nothing but a child, barely able to grasp the concept of consequences.")
		Event:ShowStoryMessage("I have that alone to thank for the dream-like episode that has remained with me all these years.")
		Event:ShowStoryMessage("One night, when my grandparents had fallen asleep, I slinked out of my bed and made my way up the old hill.")
		Event:ShowStoryMessage("For some reason, that old vine-covered house seemed to be the final frontier of mystery, innocuous though its reality may have been.")
		Event:ShowStoryMessage("I don't know what I was expecting to find in that house...")
		waitUntil("Messages read")
		
		Event:LerpStoryColor(0,0,0,0,3)
		Event:FadeMusic(0, 3)
		waitSeconds(3.5)
		
		Event:ShowStoryMessage("Whatever it was that I was expecting, I didn't find it.")
		waitUntil("Messages read")
		--Deposit player in the game
		abortAllCoroutines()
		Event:NewGame()
	end)
end





function badEnding()
	runCoroutine(function()
		Event:PlaySong("Bloom, Discord")
		waitSeconds(2)
		Event:ShowStoryMessage("That was when I came face to face with it.")
		waitUntil("Messages read")
		Event:SetStoryImage("Graphics/Cutscenes/badending1")
		Event:LerpStoryColor(1,1,1,1,4)
		Event:ShowStoryMessage("Covered in occult markings of indeterminable origin, the obelisk stood over me like a nightmare.")
		Event:ShowStoryMessage("It was at that very moment that I knew that I had made a mistake.  How could a child have possibly understood the implications of such a structure?")
		Event:ShowStoryMessage("I was paralyzed, and the unholy thing seemed to speak to me . . . ")
		Event:ShowStoryMessage("They were words of alien tongues, snapping and slurping against the sounds of blasphemy.")
		waitUntil("Messages read")
		Event:LerpStoryColor(0,0,0,0,3)
		waitSeconds(1.5)
		Event:ShowStoryMessage("I ran as fast as I could.")
		waitUntil("Messages read")
		waitSeconds(1.5)
		Event:SetStoryImage("Graphics/Cutscenes/badending2")
		Event:LerpStoryColor(1,1,1,1,3)
		waitSeconds(1)
		Event:ShowStoryMessage("I awoke the next morning in my bed.  My head hurt, and my heart was still pounding.")
		Event:ShowStoryMessage("I told my grandfather what had happened.  He told me that it was only a bad dream.")
		waitUntil("Messages read")
		waitSeconds(1)
		Event:ShowStoryMessage("At first, I could not accept that explanation.  Now, I'm inclined to believe that that was the truth.")
		Event:ShowStoryMessage("Now that I have kids of my own, I know first hand how out of control their imaginations can be.")
		Event:ShowStoryMessage("Even if it was all a dream, even now I find myself shivering when I think about the thing I saw beneath the house.")
		waitUntil("Messages read")
		Event:LerpStoryColor(0,0,0,0,4)
		waitSeconds(5)
		Event:ShowStoryMessage("And the cycle continues.")
		waitUntil("Messages read")
		
		-- Roll credits

	end)
end


function goodEnding()
	runCoroutine(function()
		Event:PlaySong("Bloom, Discord - Sweet Briar Version")
		waitSeconds(2)
		Event:ShowStoryMessage("I don't know why I did what I did.")
		Event:ShowStoryMessage("As I put the last of the flowers on the grave -- a grave that could not have actually been there -- something happened.")
		waitUntil("Messages read")
		
		Event:SetStoryImage("Graphics/Cutscenes/goodending1")
		Event:LerpStoryColor(1,1,1,1,4)
		Event:ShowStoryMessage("The terrible pulsating obelisk seemed to vanish, as though being washed away by a blue light.")
		Event:ShowStoryMessage("The light seemed to bloom all around, and for a moment I felt at peace.")
		Event:ShowStoryMessage("I caught a glimpse of something out of the corner of my eye.  I can't say for sure whether or not I really saw it, but I thought it was the girl.")
		Event:ShowStoryMessage("I wasn't scared.  I felt . . . Relieved.")
		waitUntil("Messages read")
		Event:LerpStoryColor(0,0,0,0,3)
		waitSeconds(1.5)
		Event:ShowStoryMessage("Soon, the light faded away and the dark foreboding I had felt before vanished along with it.")
		Event:ShowStoryMessage("Before the light had completely vanished, I was certain I heard a girl's voice whispering in my ear:")
		Event:ShowStoryMessage("THANK YOU.  PLEASE REMEMBER MY NAME.")
		waitUntil("Messages read")
		waitSeconds(1.5)
		Event:SetStoryImage("Graphics/Cutscenes/goodending2")
		Event:LerpStoryColor(1,1,1,1,3)
		waitSeconds(1)
		Event:ShowStoryMessage("I left, feeling as though whatever had transpired in that house was now laid to rest, just as the girl's spirit had been.")
		Event:ShowStoryMessage("On the way out, I noticed that the eerie lights, too, had found their peace and extinguished themselves.")
		Event:ShowStoryMessage("There was no doubt that whatever had happened to that girl, whatever had caused her spirit such unrest, had been forgiven.")
		waitUntil("Messages read")
		Event:LerpStoryColor(0,0,0,0,2)
		waitSeconds(2)
		Event:SetStoryImage("Graphics/Cutscenes/goodending3")
		Event:LerpStoryColor(1,1,1,1,2)
		Event:ShowStoryMessage("I awoke the next morning in my bed.  My first thought was that it had all been an especially vivid dream.")
		Event:ShowStoryMessage("I may have believed that, too, were it not for the puzzlebox and photograph sitting beside my bed.")
		waitUntil("Messages read")
		waitSeconds(1.5)
		Event:LerpStoryColor(0,0,0,0,3)
		Event:ShowStoryMessage("I told my grandfather about what had happened.  Unsurprisingly, he did not believe me.")
		waitSeconds(3)
		Event:SetStoryImage("Graphics/Cutscenes/goodending4")
		Event:ShowStoryMessage("With a laugh, he showed me the old hill that I told him I had climbed the previous night.")
		waitSeconds(1)
		Event:LerpStoryColor(1,1,1,1,3)
		waitSeconds(1)
		Event:ShowStoryMessage("There was no house there.")
		Event:ShowStoryMessage("He told me, in fact, that he could never remember a time when there was a house on that hill.")
		waitUntil("Messages read")
		
		waitSeconds(1.5)
		Event:ShowStoryMessage("I eventually decided that I had been chosen.  The girl, for some reason, wanted me alone to remember.")
		Event:ShowStoryMessage("As little sense as it made to me at the time, the photo and the puzzlebox were proof of what had transpired.")
		waitUntil("Messages read")
		Event:LerpStoryColor(0,0,0,0,1.5)
		waitSeconds(1.5)
		Event:SetStoryImage("Graphics/Cutscenes/goodending5")
		Event:LerpStoryColor(1,1,1,1,1.5)
		Event:ShowStoryMessage("I threw the strange coin into the river, where it could not cause any more trouble.  I put the photograph in the box, and swore that I would abide by her wish.")
		Event:ShowStoryMessage("I still have that box among my mementos.")
		waitSeconds(1.5)
		Event:ShowStoryMessage("Now I have kids of my own.")
		Event:ShowStoryMessage("When my first daughter was born, I made sure that the girl's name would live on.")
		waitUntil("Messages read")
		
		Event:LerpStoryColor(0,0,0,0,4)
		Event:FadeMusic(0, 5)
		waitSeconds(5)
		Event:ShowStoryMessage("I never have -- and never will -- forget the name Eglantine.")
		waitUntil("Messages read")
		
		-- Roll credits

	end)
end