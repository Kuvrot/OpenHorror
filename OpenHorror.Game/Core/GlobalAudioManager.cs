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
    public class GlobalAudioManager : SyncScript
    {
        #region singleton
        public static GlobalAudioManager Instance;
        public override void Start()
        {
            Instance = this;
        }

        public Sound openDoor , closeDoor, lockDoor, unlockDoor, takeItem, inspectItem, readDocument, pressButton;

        #endregion

        public override void Update()
        {
        }
    }
}
