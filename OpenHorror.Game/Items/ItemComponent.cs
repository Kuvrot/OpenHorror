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
using System.Security.Cryptography.X509Certificates;
using System.IO;
using OpenHorror.Trigger;

namespace OpenHorror.Items
{
    public enum ItemType
    {
        key,
        document,
        inspectable,
        nonInspectable,
        triggerEvent
    }

    public class ItemComponent : SyncScript
    {
        public ItemType ItemType { get; set; }
        public ushort itemId = 0;
        public string itemName = "Object";
        public string itemDescription = "This is an object...";
        public bool isPickable = false; //This variable indicates if the item can be pickable after inspection or not.
        public Quaternion rotationInspection; //Rotation of the object when is being inspected
        public string notificationText = "Picked item...";

        public string documentText = "";

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
            GameManager.Instance.GetUI().PushNotification(Language.Instance.Translate(notificationText));
            GameManager.Instance.GetAudioManager().PlaySound(GlobalAudioManager.Instance.takeItem);
        }

        public void Inspect ()
        {
            if (ItemType == ItemType.triggerEvent)
            {
                GameManager.Instance.GetUI().PushNotification(Language.Instance.Translate(notificationText));
                Entity.Get<TriggerEventManually>().UpdateMap();
                return;
            }

            if (ItemType == ItemType.nonInspectable)
            {
                GameManager.Instance.GetUI().PushNotification(Language.Instance.Translate(notificationText));
                return;
            }

            if (!GameManager.Instance.IsInspecting())
            {
                GameManager.Instance.inspectPosition.Entity.Transform.Scale = Entity.Transform.Scale;
                GameManager.Instance.inspectPosition.Entity.Transform.Rotation = rotationInspection;
                Entity.Remove(modelComponent);
                GameManager.Instance.inspectPosition.Entity.Add(modelComponent);
                GameManager.Instance.SetInspecting(true , this);
                if (ItemType == ItemType.document)
                {
                    GameManager.Instance.GetAudioManager().PlaySound(GlobalAudioManager.Instance.readDocument);
                }
                else
                {
                    GameManager.Instance.GetAudioManager().PlaySound(GlobalAudioManager.Instance.inspectItem);
                }
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
