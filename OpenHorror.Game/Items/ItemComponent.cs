using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using OpenHorror.Core;
using Stride.Rendering;
using Stride.Core.Shaders.Ast;
using System.ComponentModel;

namespace OpenHorror.Items
{
    public enum ItemType
    {
        key,
        document,
        inspectable,
        nonInspectable
    }

    public class ItemComponent : SyncScript
    {
        public ItemType ItemType { get; set; }
        public ushort itemId = 0;
        public String itemName = "Object";
        public String itemDescription = "This is an object...";
        public bool isPickable = false; //This variable indicates if the item can be pickable after inspection or not.
        public Quaternion rotationInspection; //Rotation of the object when is being inspected
        public String notificationText = "Picked item...";

        public String documentText = "";

        private Vector3 startPosition , inspectPosition;
        private ModelComponent  modelComponent;
        public override void Start()
        {
            startPosition = Entity.Transform.Position;
            inspectPosition = GameManager.Instance.inspectPosition.Position;
            modelComponent = Entity.Get<ModelComponent>();
        }

        public override void Update()
        {
        }

        public void PickItem()
        {
            ItemComponent itemComponent = this;
            GameManager.Instance.playerInventorySystem.Inventory.Add(itemComponent);
            Entity.Get<ModelComponent>().Enabled = false;
            Entity.Remove(this);
            GameManager.Instance.GetUI().PushNotification(notificationText);
        }

        public void Inspect ()
        {
            if (ItemType == ItemType.nonInspectable)
            {
                GameManager.Instance.GetUI().PushNotification(notificationText);
                return;
            }

            if (!GameManager.Instance.IsInspecting())
            {
                GameManager.Instance.inspectPosition.Entity.Transform.Scale = Entity.Transform.Scale;
                GameManager.Instance.inspectPosition.Entity.Transform.Rotation = rotationInspection;
                Entity.Remove(modelComponent);
                GameManager.Instance.inspectPosition.Entity.Add(modelComponent);
                GameManager.Instance.SetInspecting(true , this);
            }
            else
            {
                GameManager.Instance.inspectPosition.Entity.Remove(modelComponent);
                Entity.Add(modelComponent);
                GameManager.Instance.inspectPosition.Entity.Transform.Scale = Vector3.One;
                GameManager.Instance.inspectPosition.Entity.Transform.Rotation = Quaternion.Zero;
                GameManager.Instance.SetInspecting(false);

                if (isPickable)
                {
                    PickItem();
                }
            }
        }

        public void SetPickable(bool isPickable)
        {
            this.isPickable = isPickable;
        }
        public bool IsPickable()
        {
            return isPickable;
        }
    }
}
