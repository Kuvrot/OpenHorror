// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using OpenHorror.Core;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Engine.Events;
using Stride.Input;
using Stride.Physics;

namespace OpenHorror.Player
{
    public class PlayerController : SyncScript
    {
        [Display("Run Speed")]
        public float MaxRunSpeed { get; set; } = 5;

        public static readonly EventKey<float> RunSpeedEventKey = new EventKey<float>();

        // This component is the physics representation of a controllable character
        private CharacterComponent character;
        private CameraComponent camera;

        private readonly EventReceiver<Vector3> moveDirectionEvent = new EventReceiver<Vector3>(PlayerInput.MoveDirectionEventKey);

        private float defaultFOV = 0;
        /// <summary>
        /// Called when the script is first initialized
        /// </summary>
        public override void Start()
        {
            base.Start();

            // Will search for an CharacterComponent within the same entity as this script
            character = Entity.Get<CharacterComponent>();
            if (character == null) throw new ArgumentException("Please add a CharacterComponent to the entity containing PlayerController!");
            camera = Entity.GetChild(0).Get<CameraComponent>();
            defaultFOV = camera.VerticalFieldOfView;
        }
        
        /// <summary>
        /// Called on every frame update
        /// </summary>
        public override void Update()
        {
            if (!GameManager.Instance.IsInspecting() && !GameManager.Instance.GetInteract())
            {
                Move();
            }
            else
            {
                camera.VerticalFieldOfView = defaultFOV;
            }
        }

        private void Move()
        {
            // Character speed
            Vector3 moveDirection = Vector3.Zero;
            moveDirectionEvent.TryReceive(out moveDirection);
            // Broadcast normalized speed
            RunSpeedEventKey.Broadcast(moveDirection.Length());

            if (Input.IsKeyDown(Keys.LeftShift))
            {
                Entity.Get<FootstepsSystem>().setRunning(moveDirection != Vector3.Zero);
                character.SetVelocity(moveDirection * MaxRunSpeed * 2f);
                if (camera.VerticalFieldOfView <= defaultFOV + 5f)
                {
                    camera.VerticalFieldOfView += 20 * (float)this.Game.UpdateTime.Elapsed.TotalSeconds;
                }
                return;
            }
            else
            {
                if (camera.VerticalFieldOfView >= defaultFOV)
                {
                    camera.VerticalFieldOfView -= 20 * (float)this.Game.UpdateTime.Elapsed.TotalSeconds;
                }
                Entity.Get<FootstepsSystem>().setWalking(moveDirection != Vector3.Zero);
                character.SetVelocity(moveDirection * MaxRunSpeed);
            }
        }
    }
}
