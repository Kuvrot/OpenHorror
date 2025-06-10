using System;
using Stride.Engine;
using Stride.Rendering.Sprites;
using Stride.UI.Controls;

namespace OpenHorror.Core
{
    public class UIManager : SyncScript
    {
        #region Singleton
        public static UIManager Instance { get; private set; }

        public override void Start()
        {
            Instance = this;
            notificationUI = playerUI.Page.RootElement.FindName("Notification") as TextBlock;
        }
        #endregion

        public byte defaultFrame = 0, keyFrame = 1, lockFrame = 2, shovelFrame = 3, lensFrame = 4, handFrame = 5;
        public UIComponent playerUI;
        public UIComponent inspectUI;
        TextBlock notificationUI;

        private float timer = 0;

        public override void Update()
        {
            if (notificationUI.Text != "")
            {
               if (timer < 3f)
               {
                    Counter();
               }
               else
               {
                   notificationUI.Text = "";
                   timer = 0;
               }
            }
        }

        public void SetCursor(byte cursor)
        {
            var UIcursors = playerUI.Page.RootElement.FindName("cursor") as ImageElement;
            var sprite = UIcursors.Source as SpriteFromSheet;
            sprite.CurrentFrame = cursor;
        }

        private void Counter()
        {
            timer += 1 * (float)this.Game.UpdateTime.Elapsed.TotalSeconds;
        }

        public void PushNotification(String notification)
        {
            notificationUI.Text = notification;
        }
    }
}
