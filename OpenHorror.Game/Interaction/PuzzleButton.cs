using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using OpenHorror.Core;

namespace OpenHorror.Interaction
{
    public class PuzzleButton : SyncScript
    {
        
        public PuzzleSystem puzzleSystem; // puzzle system that this button belongs to
        public string value = "";
        public override void Start()
        {
            if (puzzleSystem == null)
            {
                puzzleSystem = Entity.GetParent().Get<PuzzleSystem>();
            }
        }

        public override void Update()
        {
        }

        public void PressButton()
        {
            puzzleSystem.PushHistory(value);
            GameManager.Instance.GetAudioManager().PlaySound(GlobalAudioManager.Instance.pressButton);
        }
    }
}
