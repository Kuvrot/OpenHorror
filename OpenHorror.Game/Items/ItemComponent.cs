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
        pickable,
        key,
        document
    }

    public class ItemComponent : SyncScript
    {
        public ItemType ItemType { get; set; }
        public ushort itemId = 0;
        public String itemName = "";
        public bool isPickable = false; //This variable indicates if the item can be pickable after inspection or not.
        public Quaternion rotationInspection; //Rotation of the object when is being inspected
        private bool inspected = false;


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
            if (isPickable)
            {
                DebugText.Print("pickable" , new Int2(300 , 300));
            }
        }

        public void PickItem()
        {
            DebugText.Print("pickedItem", new Int2(300, 300));
        }

        public void Inspect ()
        {
            
            if (!GameManager.Instance.IsInspecting())
            {
                GameManager.Instance.inspectPosition.Entity.Transform.Scale = Entity.Transform.Scale;
                GameManager.Instance.inspectPosition.Entity.Transform.Rotation = rotationInspection;
                Entity.Remove(modelComponent);
                GameManager.Instance.inspectPosition.Entity.Add(modelComponent);
                GameManager.Instance.SetInspecting(true);
            }
            else
            {
                GameManager.Instance.inspectPosition.Entity.Remove(modelComponent);
                Entity.Add(modelComponent);
                GameManager.Instance.inspectPosition.Entity.Transform.Scale = Vector3.One;
                GameManager.Instance.inspectPosition.Entity.Transform.Rotation = Quaternion.Zero;
                GameManager.Instance.SetInspecting(false);
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
