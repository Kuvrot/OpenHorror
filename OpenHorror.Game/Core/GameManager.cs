using Stride.Engine;
using OpenHorror.Items;
using Stride.UI.Controls;
using Stride.Input;
using Stride.UI.Panels;
using OpenHorror.Player;

namespace OpenHorror.Core
{
    public class GameManager : SyncScript
    {
        #region Singleton
        public static GameManager Instance { get; private set; }

        public override void Start()
        {
            Instance = this;
            uiManager = Entity.Get<UIManager>();
            playerInventorySystem = Entity.Get<PlayerInventorySystem>();
        }
        #endregion

        public TransformComponent inspectPosition;
        private UIManager uiManager;
        private bool isInspecting;
        private ItemComponent itemComponent; //current item being inspected
        public PlayerInventorySystem playerInventorySystem;

        public override void Update()
        {
            if (isInspecting)
            {
                uiManager.inspectUI.Enabled = true;
                TextBlock itemName = uiManager.inspectUI.Page.RootElement.FindName("ItemName") as TextBlock;
                TextBlock readDocument = uiManager.inspectUI.Page.RootElement.FindName("ReadDocument") as TextBlock;
                TextBlock takeItem = uiManager.inspectUI.Page.RootElement.FindName("TakeItem") as TextBlock;
                TextBlock documentText = uiManager.inspectUI.Page.RootElement.FindName("DocumentText") as TextBlock;
                StackPanel documentPanel = uiManager.inspectUI.Page.RootElement.FindName("DocumentPanel") as StackPanel;

                itemName.Text = itemComponent.itemName;

                if (itemComponent.isPickable)
                {
                    takeItem.Opacity = 1;
                }
                else
                {
                    takeItem.Opacity = 0;
                }

                if (itemComponent.ItemType == ItemType.document)
                {
                    readDocument.Opacity = 1;
                    documentText.Text = itemComponent.documentText;

                    if (Input.IsKeyReleased(Keys.R) && documentPanel.Opacity == 0)
                    {
                        documentPanel.Opacity = 1;
                    }
                    else if (Input.IsKeyReleased(Keys.R) && documentPanel.Opacity == 1)
                    {
                        documentPanel.Opacity = 0;
                    }
                }
                else
                {
                    readDocument.Opacity = 0;
                    documentText.Text = "";
                    documentPanel.Opacity = 0;
                }   
            }
            else
            {
                uiManager.inspectUI.Enabled = false;
            }
        }


        public UIManager GetUI()
        {
            return uiManager;
        }

        public void SetInspecting(bool isInspecting , ItemComponent itemComponent)
        {
            this.isInspecting = isInspecting;
            this.itemComponent = itemComponent;
        }

        public void SetInspecting(bool isInspecting)
        {
            this.isInspecting = isInspecting;
        }

        public bool IsInspecting()
        {
            return isInspecting;
        }
    }
}
