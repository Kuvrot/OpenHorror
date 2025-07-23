using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using OpenHorror.Core;
using Stride.Audio;

namespace OpenHorror.Trigger
{

    public enum TriggerTypes
    {
        sound,
        mapUpdate,
        sceneChange
    }

    public class TriggerEventByDistance : SyncScript
    {
        public float triggerDistance = 4;
        public TriggerTypes TriggerType { get; set;}
        public Sound triggerSound;

        public bool isActive = true;

        private bool isTriggered = false;

        public override void Start()
        {
            // Initialization of the script.
        }

        public override void Update()
        {
           if (isActive)
            {
                float distance = Vector3.Distance(this.Entity.Transform.Position, GameManager.Instance.Player.Position);
                if (distance <= triggerDistance)
                {
                    if (!isTriggered)
                    {
                        ExecuteTrigger();
                        isTriggered = true;
                    }
                }
            }
        }

        private void ExecuteTrigger()
        {
            if (TriggerType == TriggerTypes.sound)
            {
                if (triggerSound != null)
                {
                    var instance = triggerSound.CreateInstance();
                    instance.Play();
                }
            }

            if (TriggerType == TriggerTypes.mapUpdate)
            {
                Entity.Get<TriggerEventManually>().UpdateMap();
            }
        }
    }
}
