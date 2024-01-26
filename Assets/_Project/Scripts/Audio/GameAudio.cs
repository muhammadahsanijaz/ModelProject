using System;
using UnityEngine;
using UnityEngine.Audio;

namespace MoonKart
{
    public class GameAudio : GameService
    {
        // PRIVATE MEMBERS

        [SerializeField] private AudioEffect _music;

        [SerializeField] private AudioMixer _masterMixer;

        // PUBLIC METHODS

        public void PlayMusic()
        {
            if (_music != null)
            {
                _music.Play();
            }
        }

        public void PlayMusic(AudioSetup music)
        {
            if (music.Clips.Length == 0)
            {
                StopMusic();
                return;
            }

            _music.Play(music, true);
        }

        public void StopMusic(bool immediate = false)
        {
            _music.Stop(immediate);
        }

        public void UpdateVolume()
        {
            if (_masterMixer == null)
                return;

            _masterMixer.SetFloat("MusicVolume", Global.RuntimeSettings.MusicVolume ? 0:-80);
            _masterMixer.SetFloat("EffectsVolume", Global.RuntimeSettings.SoundVolume?0:-80);
        }

        // GameService INTERFACE

        protected override void OnActivate()
        {
            base.OnActivate();

            UpdateVolume();
        }
    }
}