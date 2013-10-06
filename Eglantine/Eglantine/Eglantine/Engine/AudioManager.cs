using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Eglantine.Engine;

#if __WINDOWS__
using NLua;
#else
using LuaInterface;
#endif

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
		public Dictionary<string, SoundEffect> Songs;

		public List<SoundEffectWrapper> LoopingSoundEffects;
		public List<SoundEffectInstance> PlayingSongs;

		// For lerping music
		bool lerpingMusic;
		float musicLerpTime;
		float musicLerpDuration;
		float startVolume;
		float endVolume;

		// For lerping soundeffects
		bool lerpingSfx;
		float sfxLerpTime;
		float sfxLerpDuration;
		float sfxstartVolume;
		float sfxendVolume;

		public AudioManager ()
		{
		}

		public void Initialize ()
		{
			LoopingSoundEffects = new List<SoundEffectWrapper>();
			PlayingSongs = new List<SoundEffectInstance>();
			LoadSoundEffects();
			LoadSongs();
		}

		public void Update (GameTime gameTime)
		{
			if (lerpingMusic)
			{
				musicLerpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
				foreach(SoundEffectInstance s in PlayingSongs)
				{
                    float newVolume = startVolume - ((startVolume - endVolume) * (musicLerpTime / musicLerpDuration));
                    s.Volume = Math.Max(0f, newVolume);
				}

				if(musicLerpTime >= musicLerpDuration)
				{
					lerpingMusic = false;
					if(endVolume < 0.1f)
						PlayingSongs.Clear();
					//MediaPlayer.Volume = endVolume;
				}
			}

			if (lerpingSfx)
			{
				sfxLerpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
				foreach(SoundEffectWrapper w in LoopingSoundEffects)
				{
					SoundEffectInstance s = w.Sound;
					s.Volume = startVolume - (sfxstartVolume - sfxendVolume) * sfxLerpTime / sfxLerpDuration;
				}
				
				if(sfxLerpTime >= sfxLerpDuration)
				{
					lerpingSfx = false;
					if(sfxendVolume < 0.1f)
						LoopingSoundEffects.Clear();
					//MediaPlayer.Volume = endVolume;
				}
			}
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
			Songs = new Dictionary<string, SoundEffect>();
			LuaTable songTable = Eglantine.MainLua.GetTable ("songs_to_load");

			string newSongName;
			SoundEffect newSong;
			for (int i = 0; i < songTable.Keys.Count; i++)
			{
				try
				{
					newSongName = (string)songTable[i+1];
					newSong = ContentLoader.Instance.Load<SoundEffect>("Audio/Music/" + newSongName);
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
                float newVolume = Math.Min(1f, volume * sfxVolume);
				SoundEffects[soundEffectName].Play(newVolume, pitch, pan);
			}
			else
			{
				Console.WriteLine("Sound effect " + soundEffectName + " not found!");
			}
		}

		public void StopSoundEffect (string soundEffectName)
		{
			Console.WriteLine ("Stop all sound effects named " + soundEffectName);
			foreach(SoundEffectWrapper s in LoopingSoundEffects.FindAll (x => x.Name == soundEffectName))
			{
				s.Sound.Stop ();
				LoopingSoundEffects.Remove (s);
			}

		}

		public void PlayLoopingSoundEffect (string soundEffectName, float volume, float pitch = 0f, float pan = 0f)
		{
			if (SoundEffects.ContainsKey (soundEffectName))
			{
				SoundEffectInstance sfx = SoundEffects[soundEffectName].CreateInstance();
				sfx.Volume = volume;
				sfx.Pitch = pitch;
				sfx.Pan = pan;
				sfx.IsLooped = true;

				// Add it to the list so it can be aborted later.
				LoopingSoundEffects.Add(new SoundEffectWrapper(soundEffectName, sfx));

				sfx.Play ();
			} else
			{
				Console.WriteLine("Sound effect " + soundEffectName + " not found!");
			}
		}

		public void StopLoopingSoundEffects ()
		{
			foreach(SoundEffectWrapper sfx in LoopingSoundEffects)
				sfx.Sound.Stop ();

			LoopingSoundEffects.Clear ();
		}

		public void SetLoopingSoundEffectVolume (string name, float volume)
		{
			foreach (SoundEffectWrapper sfx in LoopingSoundEffects)
			{
				if(sfx.Name == name)
					sfx.Sound.Volume = volume;
			}
		}

		public void PlaySong (string songName, float volume, bool looping = true, float pitch = 0f, float pan = 0f)
		{
			if (Songs.ContainsKey (songName))
			{
				SoundEffectInstance song = Songs[songName].CreateInstance();
				song.Volume = volume;
				song.Pitch = pitch;
				song.Pan = pan;

				if(looping)
					song.IsLooped = true;
				
				// Add it to the list so it can be aborted later.
				PlayingSongs.Add(song);

				song.Play ();
			} else
			{
				Console.WriteLine("Song " + songName + " not found!");
			}
		}

		public void StopMusic ()
		{
			foreach (SoundEffectInstance s in PlayingSongs)
			{
				s.Stop ();
			}
			PlayingSongs.Clear ();

			// For Windows:
			//MediaPlayer.Stop ();
		}

		public void FadeMusic (float targetVolume, float duration)
		{
			if (PlayingSongs.Count > 0)
			{
				startVolume = PlayingSongs[0].Volume;
				endVolume = targetVolume;
				musicLerpDuration = duration;
				musicLerpTime = 0f;
				
				lerpingMusic = true;
			}
		}

		public void FadeSfx (float targetVolume, float duration)
		{
			if (LoopingSoundEffects.Count > 0)
			{
				sfxstartVolume = LoopingSoundEffects[0].Sound.Volume;
				sfxendVolume = targetVolume;
				sfxLerpDuration = duration;
				sfxLerpTime = 0f;
				
				lerpingSfx = true;
			}
		}

		public class SoundEffectWrapper
		{
			public SoundEffectInstance Sound;
			public string Name;

			public SoundEffectWrapper(string name, SoundEffectInstance sound)
			{
				Sound = sound;
				Name = name;
			}
		}
	}
}

