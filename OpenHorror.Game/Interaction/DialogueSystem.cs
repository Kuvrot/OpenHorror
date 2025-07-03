using System.Collections.Generic;
using Stride.Engine;
using OpenHorror.Core;

namespace OpenHorror.Interaction
{
    public class DialogueSystem : SyncScript
    {
        public List<string> DialoguesList = [];
        public float timeBetweenDialogues = 5;
        private bool dialogueInitiated = false;
        private int currentDialogue = 0;
        private float clock = 0;
        private string currentString;
        public override void Start()
        {
            if (DialoguesList.Count > 0)
            {
                currentString = DialoguesList[currentDialogue];
            }

            clock = timeBetweenDialogues;
        }

        public override void Update()
        {
            if (dialogueInitiated)
            {
                if (Counter())
                {
                    if (currentDialogue < DialoguesList.Count)
                    {
                        currentString = DialoguesList[currentDialogue];
                        GameManager.Instance.GetUI().PrintText(currentString);
                        currentDialogue++;
                    }
                    else
                    {
                        GameManager.Instance.SetInteract(false);
                        currentDialogue = 0;
                        currentString = "";
                        GameManager.Instance.GetUI().PrintText(currentString);
                        clock = timeBetweenDialogues;
                        dialogueInitiated = false;
                    }
                }
            }
        }

        public void Interact()
        {
            dialogueInitiated = true;
            GameManager.Instance.SetInteract(true);
        }

        private bool Counter()
        {
            if (clock >= timeBetweenDialogues)
            {
                clock = 0;
                return true;
            }
            else
            {
                clock += 1 * (float)this.Game.UpdateTime.Elapsed.TotalSeconds;
            }

            return false;
        }
    }
}
