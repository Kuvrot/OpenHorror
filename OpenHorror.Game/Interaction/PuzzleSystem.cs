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
        private bool puzzleSolved = false;

        public DoorSystem unlockDoor;

        public override void Start()
        {
            // Initialization of the script.
        }

        public override void Update()
        {
            if (history.Contains(password) && !puzzleSolved)
            {
                unlockDoor.Unlock();
                history = "";
                puzzleSolved = true;
            }
        }

        public void PushHistory(string history)
        {
            this.history += history;
        }
    }
}
