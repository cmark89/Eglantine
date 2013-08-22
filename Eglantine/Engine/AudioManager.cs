using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using LuaInterface;
using Eglantine.Engine;

namespace Eglantine
{
	public class AudioManager
	{
		// Yet another singleton to add to the fire
		private static AudioManager _instance;
		public static AudioManager Instance
		{
			get
			{
				if(_instance == null)
					_instance = new AudioManager();

				return _instance;
			}
		}

		private float sfxVolume = 1f;
		private float musicVolume = 1f;

		public Dictionary<string, SoundEffect> SoundEffects;
		public Dictionary<string, Song> Songs;

		public List<SoundEffectInstance> LoopingSoundEffects;
		public List<Song> PlayingSongs;

		public AudioManager ()
		{
		}

		public void Initialize ()
		{
			LoopingSoundEffects = new List<SoundEffectInstance>();
			PlayingSongs = new List<Song>();
			LoadSoundEffects();
			LoadSongs();
		}

		public void LoadSoundEffects()
		{
			SoundEffects = new Dictionary<string, SoundEffect>();
			LuaTable soundEffectsTable = Eglantine.MainLua.GetTable("sound_effects_to_load");

			string newSoundEffectName;
			SoundEffect newSoundEffect;
			for(int i = 0; i < soundEffectsTable.Keys.Count; i++)
			{
				try
				{
					newSoundEffectName = (string)soundEffectsTable[i+1];
					newSoundEffect = ContentLoader.Instance.Load<SoundEffect>("Audio/SoundEffects/" + newSoundEffectName);
					SoundEffects.Add(newSoundEffectName, newSoundEffect);
				}
				catch(Exception e)
				{
					Console.WriteLine("Error loading sound effect: " + e.ToString());
				}
			}
		}

		public void LoadSongs ()
		{
			Songs = new Dictionary<string, Song>();
			LuaTable songTable = Eglantine.MainLua.GetTable ("songs_to_load");

			string newSongName;
			Song newSong;
			for (int i = 0; i < songTable.Keys.Count; i++)
			{
				try
				{
					newSongName = (string)songTable[i+1];
					newSong = ContentLoader.Instance.Load<Song>("Audio/Music/" + newSongName);
					Songs.Add(newSongName, newSong);
				}
				catch(Exception e)
				{
					Console.WriteLine("Error loading song: " + e.ToString());
				}
			}
		}

		public void PlaySoundEffect (string soundEffectName, float volume, float pitch = 0f, float pan = 0f)
		{
			if (SoundEffects.ContainsKey (soundEffectName))
			{
				SoundEffects[soundEffectName].Play(volume * sfxVolume, pitch, pan);
			} 
			else
			{
				Console.WriteLine("Sound effect " + soundEffectName + " not found!");
			}
		}

		public void PlayLoopingSoundEffect (string soundEffectName, float volume, float pitch = 0f, float pan = 0f)
		{
			if (SoundEffects.ContainsKey (soundEffectName))
			{
				SoundEffectInstance sfx = new SoundEffectInstance(SoundEffects[soundEffectName]);
				sfx.Volume = volume;
				sfx.Pitch = pitch;
				sfx.Pan = pan;
				sfx.IsLooped = true;

				// Add it to the list so it can be aborted later.
				LoopingSoundEffects.Add(sfx);

				sfx.Play ();
			} else
			{
				Console.WriteLine("Sound effect " + soundEffectName + " not found!");
			}
		}

		public void StopLoopingSoundEffects ()
		{
			foreach(SoundEffectInstance sfx in LoopingSoundEffects)
				sfx.Stop ();

			LoopingSoundEffects.Clear ();
		}

		public void PlaySong (string songName, float volume, bool looping = true, float pitch = 0f, float pan = 0f)
		{
			if (Songs.ContainsKey (songName))
			{
				MediaPlayer.Volume = volume * musicVolume;
				MediaPlayer.IsRepeating = true;
				MediaPlayer.Play(Songs[songName]);
				SoundEffects[songName].Play(volume * sfxVolume, pitch, pan);
			} else
			{
				Console.WriteLine("Song " + songName + " not found!");
			}
		}
	}
}

