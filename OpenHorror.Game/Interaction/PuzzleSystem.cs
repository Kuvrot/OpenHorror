using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace OpenHorror.Interaction
{
    public class PuzzleSystem : SyncScript
    {
        public string password = "";
        private string history = "";

        public DoorSystem unlockDoor;

        public override void Start()
        {
            // Initialization of the script.
        }

        public override void Update()
        {
            if (history.Contains(password))
            {
                unlockDoor.Unlock();
                history = "";
            }
        }

        public void PushHistory(string history)
        {
            this.history += history;
        }
    }
}
