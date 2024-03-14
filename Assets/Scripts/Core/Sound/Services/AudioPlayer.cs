using System.Collections.Generic;
using Core.AssetManagement;
using Core.ECS;
using Core.Infrastructure;
using JetBrains.Annotations;
using Scellecs.Morpeh;
using SimpleInject;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.Sound
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public abstract class AudioPlayer : IInitializer
	{
		private readonly UnityCallbacksReceiver _callbacksReceiver;
		
		protected AudioSourceCreator AudioSourceCreator;

		protected List<AudioSource> Sources;

		protected abstract SoundType Type { get; }

		public World World { get; set; }

		protected AudioPlayer(UnityCallbacksReceiver callbacksReceiver)
		{
			_callbacksReceiver = callbacksReceiver;
		}

		public void OnAwake()
		{
			Sources = AudioSourceCreator.CreateSoundSources(Type);

			_callbacksReceiver.ApplicationPaused += SwitchMuteAllAudio;
			_callbacksReceiver.ApplicationFocus += SwitchMuteAllAudio;
		}

		public abstract void Play(Entity entity);

		public void Stop()
		{
			foreach (var source in Sources)
			{
				if (!source.isPlaying)
					continue;
				
				source.Stop();
				source.clip = null;
			}
		}

		public void SwitchSourcesActive(bool active)
		{
			foreach (AudioSource audioSource in Sources)
				audioSource.mute = !active;
		}

		public void SwitchMuteAllAudio(bool isActive)
		{
			AudioListener.pause = !isActive;
		}

		[CanBeNull]
		protected AudioSource FindFree()
		{
			foreach (var audioSource in Sources)
			{
				if (!audioSource.isPlaying)
					return audioSource;
			}

			return null;
		}

		protected void Play(Entity entity, AudioSource source)
		{
			source.clip = entity.GetComponent<AudioClipComponent>().Value;
			source.volume = entity.GetComponent<Volume>().Value;
			source.pitch = entity.GetComponent<Pitch>().Value;
			source.loop = entity.Has<Looped>();
			source.Play();
			
			entity.SetComponent(new Destroyed());
		}

		public void Dispose()
		{
			// foreach (var source in Sources) 
				// Object.Destroy(source);
			Sources = null;
		}
	}
}