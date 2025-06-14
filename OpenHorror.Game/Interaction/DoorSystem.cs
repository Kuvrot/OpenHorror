using OpenHorror.Core;
using Stride.Core.Mathematics;
using Stride.Engine;


namespace OpenHorror.Interaction
{
    public class DoorSystem : SyncScript
    {
        public float OpenAngle = 90f;
        public float Speed = 90f;
        public bool invertDirection = false;

        public int idDoor = 0; //If the door is locked the id door should match with the id key used
        public bool isLocked = false;
        public string lockedNotification = "Door is locked..."; //In case the door is locked 
        public string unlockedNotification = "The key fits";

        private bool isOpening = false;
        private bool isMoving = false;
        private float currentAngle = 0f;

        public override void Start()
        {
        }

        public override void Update()
        {
            if (isMoving)
            {
                float rotationStep = Speed * (float)Game.UpdateTime.Elapsed.TotalSeconds;
                if (isOpening)
                {
                    currentAngle += rotationStep;
                    if (currentAngle >= OpenAngle)
                    {
                        rotationStep -= (currentAngle - OpenAngle);
                        currentAngle = OpenAngle;
                        isMoving = false;
                    }
                }
                else
                {
                    currentAngle -= rotationStep;
                    if (currentAngle <= 0f)
                    {
                        rotationStep += currentAngle;
                        currentAngle = 0f;
                        isMoving = false;
                    }
                    rotationStep = -rotationStep;
                }

                var rotation = Entity.Transform.RotationEulerXYZ;

                if (invertDirection)
                {
                    rotation.Y -= MathUtil.DegreesToRadians(rotationStep);
                }
                else
                {
                    rotation.Y += MathUtil.DegreesToRadians(rotationStep);
                }

                Entity.Transform.RotationEulerXYZ = rotation;
            }
        }

        public void Interact()
        {
            if (isLocked)
            {
                if (CheckIfPlayerHasKey())
                {
                    Unlock();
                }
                else
                {
                    GameManager.Instance.GetUI().PushNotification(lockedNotification);
                    return;
                }
            }

            isOpening = !isOpening;
            isMoving = true;
        }

        public void Unlock()
        {
            GameManager.Instance.GetUI().PushNotification(unlockedNotification);
            isLocked = false;
        }

        private bool CheckIfPlayerHasKey()
        {
            for (int i = 0; i < GameManager.Instance.playerInventorySystem.Inventory.Count; i++)
            {
                if (GameManager.Instance.playerInventorySystem.Inventory[i].ItemType == Items.ItemType.key 
                    && GameManager.Instance.playerInventorySystem.Inventory[i].itemId == idDoor)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
