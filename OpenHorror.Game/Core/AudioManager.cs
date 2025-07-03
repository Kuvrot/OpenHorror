using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Audio;

namespace OpenHorror.Core
{
    public class AudioManager : SyncScript
    {
        public override void Start()
        {
        }

        public override void Update()
        {
        }

        public void PlaySound(Sound sound)
        {
            if (sound != null)
            {
                SoundInstance soundInstance = sound.CreateInstance();
                soundInstance.Play();
            }
        }

        public async void PlaySoundOnce(Sound sound)
        {
            if (sound != null)
            {
                SoundInstance soundInstance = sound.CreateInstance();
                await soundInstance.ReadyToPlay();
                soundInstance.Play();
            }
        }

        //This method plays the sound but with a random pitch, so every time makes a "different" sound
        public void PlaySoundWithRandomPitch(Sound sound)
        {
            if (sound != null)
            {
                var soundInstace = sound.CreateInstance();
                if (new Random().Next(0, 99) >= 33)
                {
                    soundInstace.Pitch = 0.7f;
                }
                else if (new Random().Next(0, 99) >= 33 + 33)
                {
                    soundInstace.Pitch = 0.5f;
                }
                soundInstace.Play();
            }
        }
    }
}
