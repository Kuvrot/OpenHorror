using Stride.Engine;
using OpenHorror.Items;
using Stride.UI.Controls;
using Stride.Input;
using Stride.UI.Panels;
using OpenHorror.Player;
using Stride.Core.Mathematics;
using Silk.NET.Core;

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
            audioManager = Entity.Get<AudioManager>();
        }
        #endregion

        public TransformComponent inspectPosition;
        private UIManager uiManager;
        private bool isInspecting;
        private ItemComponent itemComponent; //current item being inspected
        private AudioManager audioManager;
        public PlayerInventorySystem playerInventorySystem;

        public bool isDebugEnabled = false;
        public override void Update()
        {
            if (isInspecting)
            {
                uiManager.inspectUI.Enabled = true;
                TextBlock itemName = uiManager.inspectUI.Page.RootElement.FindName("ItemName") as TextBlock;
                TextBlock readDocument = uiManager.inspectUI.Page.RootElement.FindName("ReadDocument") as TextBlock;
                TextBlock takeItem = uiManager.inspectUI.Page.RootElement.FindName("TakeItem") as TextBlock;
                TextBlock documentText = uiManager.inspectUI.Page.RootElement.FindName("DocumentText") as TextBlock;
                TextBlock itemDescription = uiManager.inspectUI.Page.RootElement.FindName("ItemDescription") as TextBlock;
                StackPanel documentPanel = uiManager.inspectUI.Page.RootElement.FindName("DocumentPanel") as StackPanel;

                itemName.Text = Language.Instance.Translate(itemComponent.itemName);
                itemDescription.Text = Language.Instance.Translate(itemComponent.itemDescription);
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
                    documentText.Text = Language.Instance.Translate(itemComponent.documentText);

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

            if (isDebugEnabled)
            {
                Debug();
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

        public AudioManager GetAudioManager()
        {
            return audioManager;
        }
        private void Debug()
        {
            //Debug menu

            DebugText.Print("[K] TO ENABLE/DISABLE AMBIENT LIGHT" , new Int2(300 , 300));
        }
    }
}
