using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using System.ComponentModel;
using Stride.Physics;


namespace OpenHorror.Interaction
{
    public class DoorSystem : SyncScript
    {
        public float OpenAngle = 90f;
        public float Speed = 90f;

        private bool isOpening = false;
        private bool isMoving = false;
        private float currentAngle = 0f;

        private StaticColliderComponent staticColliderComponent;
        public override void Start()
        {
           //staticColliderComponent = Entity.Get<StaticColliderComponent>();
        }

        public override void Update()
        {
            // Animación de apertura o cierre
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
                rotation.Y += MathUtil.DegreesToRadians(rotationStep);
                Entity.Transform.RotationEulerXYZ = rotation;
            }
        }

        public void Interact()
        {
            isOpening = !isOpening;
            isMoving = true;
        }
    }
}
