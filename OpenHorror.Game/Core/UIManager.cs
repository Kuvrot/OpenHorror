using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Graphics;
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
        }
        #endregion

        public byte defaultFrame = 0, keyFrame = 1, lockFrame = 2, shovelFrame = 3, lensFrame = 4, handFrame = 5;
        public UIComponent playerUI;

        public override void Update()
        {
            // Do stuff every new frame
        }

        public void SetCursor(byte cursor)
        {
            var UIcursors = playerUI.Page.RootElement.FindName("cursor") as ImageElement;
            var sprite = UIcursors.Source as SpriteFromSheet;
            sprite.CurrentFrame = cursor;
        }
    }
}
