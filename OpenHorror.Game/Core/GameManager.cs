using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace OpenHorror.Core
{
    public class GameManager : SyncScript
    {
        #region Singleton
        public static GameManager Instance { get; private set; }

        public override void Start()
        {
            Instance = this;
        }
        #endregion

        public TransformComponent inspectPosition;
        private UIManager uiManager;
        private bool isInspecting;

        public override void Update()
        {
            uiManager = Entity.Get<UIManager>();
        }


        public UIManager GetUI()
        {
            return uiManager;
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
