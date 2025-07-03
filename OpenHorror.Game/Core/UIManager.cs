using System;
using Stride.Core.Mathematics;
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
        private float notificationTime = 3;
        private bool isNotification = false;

        public override void Update()
        {
            DebugText.Print(isNotification.ToString(), new Int2(300, 300));

            if (notificationUI.Text != "" && isNotification)
            {
               if (timer < notificationTime)
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
            isNotification = true;
            notificationTime = 3;
            notificationUI.Text = notification;
        }

        public void PushNotificationSpecificTime (String notification, float time)
        {
            isNotification = true;
            notificationTime = time;
            notificationUI.Text = notification;
        }

        public void PrintText(String text)
        {
            isNotification = false;
            notificationUI.Text = text;
        }
    }
}
