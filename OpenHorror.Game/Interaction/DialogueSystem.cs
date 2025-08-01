﻿using System.Collections.Generic;
using Stride.Engine;
using OpenHorror.Core;
using System;
using Stride.Core.Mathematics;
using Stride.Audio;
using Silk.NET.Core;

namespace OpenHorror.Interaction
{
    public class DialogueSystem : SyncScript
    {
        public List<string> DialoguesList = [];
        public float timeBetweenDialogues = 2;
        public float timeBetweenLetters = 0.15f;
        public bool lookPlayer = true;
        private bool dialogueInitiated = false;
        private bool endDialogue = false;
        private int currentDialogue = 0;
        private int currentLetter = 0;
        private float clock = 0;
        private float letterTick = 0;
        private string currentString;

        public Sound voiceSound;
        private bool playerTalking = false;
        public override void Start()
        {
        }

        public override void Update()
        {
            if (dialogueInitiated)
            {
                if (Counter2() && !endDialogue)
                {
                    if (currentString != Language.Instance.Translate(DialoguesList[currentDialogue]))
                    {
                        if (!DialoguesList[currentDialogue].Contains("player.dialogue"))
                        {
                            Entity.Get<AudioManager>().PlaySoundWithRandomPitch(voiceSound);
                        }
                        currentString += Language.Instance.Translate(DialoguesList[currentDialogue])[currentLetter];
                        GameManager.Instance.GetUI().PrintText(Language.Instance.Translate(currentString));
                        currentLetter++;
                    }
                    else
                    {
                        currentString = "";
                        currentLetter = 0;
                        endDialogue = true;
                    }
                }

                if (endDialogue)
                {
                    if (Counter())
                    {
                        if (currentDialogue == DialoguesList.Count - 1)
                        {
                            GameManager.Instance.SetInteract(false);
                            currentDialogue = 0;
                            currentLetter = 0;
                            GameManager.Instance.GetUI().PrintText("");
                            clock = timeBetweenDialogues;
                            dialogueInitiated = false;
                        }
                        else
                        {
                            currentDialogue++;
                            GameManager.Instance.GetUI().PrintText("");
                            endDialogue = false;
                        }
                    }  
                }
            }

            if (lookPlayer)
            {
                LookTarget();
            }
        }

        public void LookTarget()
        {
            Vector2 lookAngle = GetLookAtAngle(Entity.Transform.Position, GameManager.Instance.Player.Position);
            Quaternion result = Quaternion.RotationYawPitchRoll(lookAngle.Y, 0, 0);
            Entity.Transform.Rotation = result;
        }

        private Vector2 GetLookAtAngle(Vector3 source, Vector3 destination)
        {
            Vector3 dist = source - destination;
            float altitude = (float)Math.Atan2(dist.Y, Math.Sqrt(dist.X * dist.X + dist.Z * dist.Z));
            float azimuth = (float)Math.Atan2(dist.X, dist.Z);
            return new Vector2(altitude, azimuth);
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

        private bool Counter2()
        {
            if (letterTick >= timeBetweenLetters)
            {
                letterTick = 0;
                return true;
            }
            else
            {
                letterTick += 1 * (float)this.Game.UpdateTime.Elapsed.TotalSeconds;
            }

            return false;
        }
    }
}
