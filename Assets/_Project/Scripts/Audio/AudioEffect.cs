using System.Collections;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace MoonKart
{
	[System.Serializable]
	public class AudioSetup
	{
		public AudioClip[]  Clips;
		public float        Volume = 1f;
		public float        MaxPitchChange;
		public float        Delay;
		public bool         Loop;
		public bool         Repeat;
		public float        FadeIn;
		public float        FadeOut;

	#if ODIN_INSPECTOR
		[ShowIf("Repeat")]
		public int          RepeatPlayCount;
		[ShowIf("Repeat")]
		public float        RepeatDelay;
	#else
		public int          RepeatPlayCount;
		public float        RepeatDelay;
	#endif
	}

	[RequireComponent(typeof(AudioSource))]
	public sealed class AudioEffect : MonoBehaviour
	{
		// PUBLIC MEMBERS

		public AudioSetup Default;

		public AudioSource AudioSource => _audioSource;
		public bool IsPlaying => _audioSource.isPlaying == true || _delayedPlayRoutine != null;
		public float BasePitch { get; set; }

		// PRIVATE MEMBERS

		private AudioSource _audioSource;
		private bool _playOnAwake;

		private int _playCount;
		private int _lastPlayedClipIndex;
		private Coroutine _delayedPlayRoutine;

		private AudioSetup _currentSetup;

		// PUBLIC METHODS

		public void Play(bool force = false)
		{
			if (force == false && IsPlaying == true)
				return;

			StartPlay(Default);
		}

		public void Play(AudioSetup setup, bool force = false)
		{
			if (force == false && IsPlaying == true)
				return;

			if (setup == _currentSetup && IsPlaying == true)
				return;

			if (setup.Clips.Length == 0)
				return;

			StartPlay(setup);
		}

		public void Stop(bool forceImmediateStop = false)
		{
			StopDelayedPlay();

			if (forceImmediateStop == false && _currentSetup != null && _currentSetup.FadeOut > 0f)
			{
				_audioSource.FadeOut(this, _currentSetup.FadeOut);
			}
			else
			{
				_audioSource.Stop();
			}
		}

		// MonoBehaviour INTERFACE

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();

			BasePitch = _audioSource.pitch;

			_playOnAwake = _audioSource.playOnAwake;
			_audioSource.playOnAwake = false;
			_audioSource.Stop();

			Default.Loop |= _audioSource.loop;

			if (Default.Clips.Length == 0 && _audioSource.clip != null)
			{
				Default.Clips = new AudioClip[] { _audioSource.clip };
			}
		}

		private void OnEnable()
		{
			_audioSource.enabled = true;

			if (_playOnAwake == true)
			{
				Play();
			}
		}

		private void OnDisable()
		{
			StopDelayedPlay();
			_audioSource.enabled = false;
		}

		// PRIVATE METHODS

		private void StartPlay(AudioSetup setup)
		{
			AudioSetup previousSetup = _currentSetup;

			if (setup != _currentSetup)
			{
				_lastPlayedClipIndex = -1;
				_currentSetup = setup;
			}

			int clipIndex = NextClipIndex(setup);

			if (clipIndex < 0)
				return;

			if (_currentSetup.Clips[clipIndex] == null)
				return; // Do not start playing if there will be nothing to play

			StopDelayedPlay();
			_playCount = 0;

			bool waitForFadeOut = IsPlaying == true && previousSetup != null && previousSetup.FadeOut > 0.01f;

			if (_currentSetup.Delay < 0.01f && waitForFadeOut == false)
			{
				PlayClip(clipIndex);
			}
			else
			{
				float delay = _currentSetup.Delay;

				if (waitForFadeOut == true)
				{
					delay += previousSetup.FadeOut;
					_audioSource.FadeOut(this, previousSetup.FadeOut);
				}

				_delayedPlayRoutine = StartCoroutine(PlayDelayed_Coroutine(delay, clipIndex));
			}
		}

		private void PlayClip(int clipIndex)
		{
			_audioSource.Stop();
			StopAllCoroutines(); // Stop audiosource fadings

			_lastPlayedClipIndex = clipIndex;

			_audioSource.clip = _currentSetup.Clips[clipIndex];
			_audioSource.volume = _currentSetup.Volume;
			_audioSource.loop = _currentSetup.Loop;
			_audioSource.pitch = BasePitch + Random.Range(-_currentSetup.MaxPitchChange, _currentSetup.MaxPitchChange);

			if (_currentSetup.FadeIn > 0f)
			{
				_audioSource.FadeIn(this, _currentSetup.FadeIn, volume: _currentSetup.Volume);
			}
			else
			{
				_audioSource.Play();
			}

			_playCount++;

			if (_currentSetup.Repeat == true && _playCount < _currentSetup.RepeatPlayCount)
			{
				_delayedPlayRoutine = StartCoroutine(PlayDelayed_Coroutine(_audioSource.clip.length + _currentSetup.RepeatDelay, clipIndex));
			}
		}

		private IEnumerator PlayDelayed_Coroutine(float delay, int clipIndex)
		{
			if (delay > 0.01f)
			{
				yield return WaitFor.Seconds(delay);
			}

			_delayedPlayRoutine = null;

			PlayClip(clipIndex);
		}

		private void StopDelayedPlay()
		{
			if (_delayedPlayRoutine != null)
			{
				StopCoroutine(_delayedPlayRoutine);
				_delayedPlayRoutine = null;
			}
		}

		private int NextClipIndex(AudioSetup setup)
		{
			if (setup.Clips.Length == 0)
			{
				Debug.LogWarningFormat("Cannot play sound on {0} - missing audio clip", gameObject.name);
				return -1;
			}

			int clipIndex = Random.Range(0, setup.Clips.Length);

			if (clipIndex == _lastPlayedClipIndex)
			{
				clipIndex = (clipIndex + 1) % setup.Clips.Length;
			}

			return clipIndex;
		}
	}
}
