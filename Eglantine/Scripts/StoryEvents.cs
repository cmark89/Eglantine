using System;
using System.Collections.Generic;
using ObjectivelyRadical.Scheduler;

namespace Eglantine.Engine
{
	public static partial class GameEvents
	{
		public static IEnumerator<ScriptPauser> openingCutscene()
		{
			EventManager.Instance.PlaySong("Bloom, Discord");
			yield return waitSeconds(2);
			EventManager.Instance.ShowStoryMessage("This is a story from my childhood.");
			EventManager.Instance.ShowStoryMessage("It's a story that lies somewhere between dream and reality.  To be honest, I can no longer remember which category to assign it to . . . ");
			yield return waitUntil("Messages read");
					
			EventManager.Instance.SetStoryImage("Graphics/Cutscenes/opening1");
			EventManager.Instance.LerpStoryColor(1,1,1,1,2);
			yield return waitSeconds(2);
					
			EventManager.Instance.ShowStoryMessage("I spent a lot of time with my grandparents in Widow's Creek when I was young.");
			EventManager.Instance.ShowStoryMessage("I have fond memories of laying in the grass, staring at the unpolluted stars.  It was there that I fell in love with stargazing.");
			EventManager.Instance.ShowStoryMessage("In Widow's Creek, the moon always rose behind a particular house on a hill.");
			EventManager.Instance.ShowStoryMessage("I didn't know it at the time, but that house belonged to an old man named Edward Weathers.");
			yield return waitUntil("Messages read");

			yield return waitSeconds(1);
			EventManager.Instance.LerpStoryColor(0,0,0,0,2);
			EventManager.Instance.ShowStoryMessage("When I got a little older, I asked my grandparents who lived in that house.");
			yield return waitSeconds(2);
			EventManager.Instance.ShowStoryMessage("They told me that it was abandoned.  Old Mr. Weathers had apparently died under bizarre circumstances not long before.");
			EventManager.Instance.ShowStoryMessage("It took a while for that to sink in.  I hadn't known him, but his death disturbed me.");
			EventManager.Instance.ShowStoryMessage("That's because sometimes, as evening turned into night, lights would go on and off in that abandoned house on the hill.");
			EventManager.Instance.SetStoryImage("Graphics/Cutscenes/opening3");
			EventManager.Instance.LerpStoryColor(1,1,1,1,4);
			yield return waitUntil("Messages read");
					
			EventManager.Instance.LerpStoryColor(0,0,0,0,5);
			EventManager.Instance.ShowStoryMessage("Had I been a little older, I would have just assumed it was vandals and teenagers treading on the old man's legacy.  Of course, I would have been wrong.");
					
			yield return waitSeconds(2);
			EventManager.Instance.ShowStoryMessage("It was during the summer of my eleventh birthday.  I remember that distinctly, because it was the last summer I would spend in Widow's Creek before both my grandparents died.");
			yield return waitUntil("Messages read");
					
			EventManager.Instance.SetStoryImage("Graphics/Cutscenes/opening4");
			EventManager.Instance.LerpStoryColor(1,1,1,1,3);
			EventManager.Instance.ShowStoryMessage("I decided that I was going to explore the house.  I was nothing but a child, barely able to grasp the concept of consequences.");
			EventManager.Instance.ShowStoryMessage("I have that alone to thank for the dream-like episode that has remained with me all these years.");
			EventManager.Instance.ShowStoryMessage("One night, when my grandparents had fallen asleep, I slinked out of my bed and made my way up the old hill.");
			EventManager.Instance.ShowStoryMessage("For some reason, that old vine-covered house seemed to be the final frontier of mystery, innocuous though its reality may have been.");
			EventManager.Instance.ShowStoryMessage("I don't know what I was expecting to find in that house...");
			yield return waitUntil("Messages read");
					
			EventManager.Instance.LerpStoryColor(0,0,0,0,3);
			EventManager.Instance.FadeMusic(0f, 3f);
			yield return waitSeconds(3.5f);
					
			EventManager.Instance.ShowStoryMessage("Whatever it was that I was expecting, I didn't find it.");
			yield return waitUntil("Messages read");

			//Deposit player in the game
			Scheduler.AbortAllCoroutines();
			EventManager.Instance.NewGame();
		}

		public static IEnumerator<ScriptPauser> badEnding()
		{
			EventManager.Instance.PlaySong("Bloom, Discord");
			yield return waitSeconds(2);
			EventManager.Instance.ShowStoryMessage("That was when I came face to face with it.");
			yield return waitUntil("Messages read");
			EventManager.Instance.SetStoryImage("Graphics/Cutscenes/badending1");
			EventManager.Instance.LerpStoryColor(1,1,1,1,4);
			EventManager.Instance.ShowStoryMessage("Covered in occult markings of indeterminable origin, the obelisk stood over me like a nightmare.");
			EventManager.Instance.ShowStoryMessage("It was at that very moment that I knew that I had made a mistake.  How could a child have possibly understood the implications of such a structure?");
			EventManager.Instance.ShowStoryMessage("I was paralyzed, and the unholy thing seemed to speak to me . . . ");
			EventManager.Instance.ShowStoryMessage("They were words of alien tongues, snapping and slurping against the sounds of blasphemy.");
			yield return waitUntil("Messages read");
			EventManager.Instance.LerpStoryColor(0,0,0,0,3);
			yield return waitSeconds(1.5f);
			EventManager.Instance.ShowStoryMessage("I ran as fast as I could.");
			yield return waitUntil("Messages read");
			yield return waitSeconds(1.5f);
			EventManager.Instance.SetStoryImage("Graphics/Cutscenes/badending2");
			EventManager.Instance.LerpStoryColor(1,1,1,1,3);
			yield return waitSeconds(1);
			EventManager.Instance.ShowStoryMessage("I awoke the next morning in my bed.  My head hurt, and my heart was still pounding.");
			EventManager.Instance.ShowStoryMessage("I told my grandfather what had happened.  He told me that it was only a bad dream.");
			yield return waitUntil("Messages read");
			yield return waitSeconds(1);
			EventManager.Instance.ShowStoryMessage("At first, I could not accept that explanation.  Now, I'm inclined to believe that that was the truth.");
			EventManager.Instance.ShowStoryMessage("Now that I have kids of my own, I know first hand how out of control their imaginations can be.");
			EventManager.Instance.ShowStoryMessage("Even if it was all a dream, even now I find myself shivering when I think about the thing I saw beneath the house.");
			yield return waitUntil("Messages read");
			EventManager.Instance.FadeMusic(0f, 5f);
			EventManager.Instance.LerpStoryColor(0,0,0,0,4);
			yield return waitSeconds(5);
			EventManager.Instance.ShowStoryMessage("And the cycle continues.");
			yield return waitUntil("Messages read");

			yield return waitSeconds(2);
			EventManager.Instance.RollCredits();
		}

		public static IEnumerator<ScriptPauser> goodEnding()
		{
			EventManager.Instance.PlaySong("Bloom, Discord - Sweet Briar Version");
			yield return waitSeconds(2);
			EventManager.Instance.ShowStoryMessage("I don't know why I did what I did.");
			EventManager.Instance.ShowStoryMessage("As I put the last of the flowers on the grave -- a grave that could not have actually been there -- something happened.");
			yield return waitUntil("Messages read");
					
			EventManager.Instance.SetStoryImage("Graphics/Cutscenes/goodending1");
			EventManager.Instance.LerpStoryColor(1,1,1,1,4);
			EventManager.Instance.ShowStoryMessage("The terrible pulsating obelisk seemed to vanish, as though being washed away by a blue light.");
			EventManager.Instance.ShowStoryMessage("The light seemed to bloom all around, and for a moment I felt at peace.");
			EventManager.Instance.ShowStoryMessage("I caught a glimpse of something out of the corner of my eye.  I can't say for sure whether or not I really saw it, but I thought it was the girl.");
			EventManager.Instance.ShowStoryMessage("I wasn't scared.  I felt . . . Relieved.");
			yield return waitUntil("Messages read");
				EventManager.Instance.LerpStoryColor(0,0,0,0,3);
			yield return waitSeconds(1.5f);
			EventManager.Instance.ShowStoryMessage("Soon, the light faded away and the dark foreboding I had felt before vanished along with it.");
			EventManager.Instance.ShowStoryMessage("Before the light had completely vanished, I was certain I heard a girl's voice whispering in my ear:");
			EventManager.Instance.ShowStoryMessage("THANK YOU.  PLEASE REMEMBER MY NAME.");
			yield return waitUntil("Messages read");
			yield return waitSeconds(1.5f);
			EventManager.Instance.SetStoryImage("Graphics/Cutscenes/goodending2");
			EventManager.Instance.LerpStoryColor(1,1,1,1,3);
			yield return waitSeconds(1);
			EventManager.Instance.ShowStoryMessage("I left, feeling as though whatever had transpired in that house was now laid to rest, just as the girl's spirit had been.");
			EventManager.Instance.ShowStoryMessage("On the way out, I noticed that the eerie lights, too, had found their peace and extinguished themselves.");
			EventManager.Instance.ShowStoryMessage("There was no doubt that whatever had happened to that girl, whatever had caused her spirit such unrest, had been forgiven.");
			yield return waitUntil("Messages read");
			EventManager.Instance.LerpStoryColor(0,0,0,0,2);
			yield return waitSeconds(2);
			EventManager.Instance.SetStoryImage("Graphics/Cutscenes/goodending3");
			EventManager.Instance.LerpStoryColor(1,1,1,1,2);
			EventManager.Instance.ShowStoryMessage("I awoke the next morning in my bed.  My first thought was that it had all been an especially vivid dream.");
			EventManager.Instance.ShowStoryMessage("I may have believed that, too, were it not for the puzzlebox and photograph sitting beside my bed.");
			yield return waitUntil("Messages read");
			yield return waitSeconds(1.5f);
			EventManager.Instance.LerpStoryColor(0,0,0,0,3);
			EventManager.Instance.ShowStoryMessage("I told my grandfather about what had happened.  Unsurprisingly, he did not believe me.");
			yield return waitSeconds(3);
			EventManager.Instance.SetStoryImage("Graphics/Cutscenes/goodending4");
			EventManager.Instance.ShowStoryMessage("With a laugh, he showed me the old hill that I told him I had climbed the previous night.");
			yield return waitSeconds(1);
			EventManager.Instance.LerpStoryColor(1,1,1,1,3);
			yield return waitSeconds(1);
			EventManager.Instance.ShowStoryMessage("There was no house there.");
			EventManager.Instance.ShowStoryMessage("He told me, in fact, that he could never remember a time when there was a house on that hill.");
			yield return waitUntil("Messages read");
					
			yield return waitSeconds(1.5f);
			EventManager.Instance.ShowStoryMessage("I eventually decided that I had been chosen.  The girl, for some reason, wanted me alone to remember.");
			EventManager.Instance.ShowStoryMessage("As little sense as it made to me at the time, the photo and the puzzlebox were proof of what had transpired.");
			yield return waitUntil("Messages read");
			EventManager.Instance.LerpStoryColor(0,0,0,0,1.5f);
			yield return waitSeconds(1.5f);
			EventManager.Instance.SetStoryImage("Graphics/Cutscenes/goodending5");
			EventManager.Instance.LerpStoryColor(1,1,1,1,1.5f);
			EventManager.Instance.ShowStoryMessage("I threw the strange coin into the river, where it could not cause any more trouble.  I put the photograph in the box, and swore that I would abide by her wish.");
			EventManager.Instance.ShowStoryMessage("I still have that box among my mementos.");
			yield return waitSeconds(1.5f);
			EventManager.Instance.ShowStoryMessage("Now I have kids of my own.");
			EventManager.Instance.ShowStoryMessage("When my first daughter was born, I made sure that the girl's name would live on.");
			yield return waitUntil("Messages read");

			EventManager.Instance.FadeMusic(0f, 5f);		
			EventManager.Instance.LerpStoryColor(0,0,0,0,4f);
			yield return waitSeconds(5f);

			EventManager.Instance.ShowStoryMessage("I never have -- and never will -- forget the name Eglantine.");
			yield return waitUntil("Messages read");

			yield return waitSeconds(2);
			EventManager.Instance.RollCredits();
		}
	}
}