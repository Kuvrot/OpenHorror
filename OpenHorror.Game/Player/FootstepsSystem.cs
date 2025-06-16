using System;
using System.Collections.Generic;
using Stride.Engine;
using OpenHorror.Core;
using Stride.Audio;
using Stride.Core;

namespace OpenHorror.Player
{
    public class FootstepsSystem : SyncScript
    {
        [Display("footsteps intervals (in seconds)")]
        public float soundSpeed = 0.25f;
        public List<Sound> sounds = [];

        private bool isWalking = false;
        private float clock = 0;
        private AudioManager audioManager;
        private float defaultSpeed = 0;

        public override void Start()
        {
            audioManager = Entity.Get<AudioManager>();
            defaultSpeed = soundSpeed;
        }

        public override void Update()
        {
            if (isWalking && sounds.Count > 0)
            {
                if (Counter())
                {
                    audioManager?.PlaySound(sounds[new Random().Next(0, sounds.Count)]);
                }
            }
        }

        public void setWalking(bool boolean)
        {
            soundSpeed = defaultSpeed;
            isWalking = boolean;
        }

        public void setRunning(bool boolean)
        {
            soundSpeed = defaultSpeed * 0.5f;
            isWalking = boolean;
        }

        private bool Counter()
        {
            if (clock >= soundSpeed)
            {
                clock = 0;
                return true;
            }

            clock += 1 * (float)Game.UpdateTime.Elapsed.TotalSeconds;

            return false;
        }
    }
}
