using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using System.Security.Policy;
using Stride.Physics;
using Stride.Audio;

namespace OpenHorror.Trigger
{
    public class TriggerEventManually : SyncScript
    {
        public List<Entity> DisableElements = [] , EnableElements = [];
        public TriggerTypes TriggerType { get; set; }

        public Sound Sound;
        public bool isActive = true;
        public override void Start()
        {
            if (TriggerType == TriggerTypes.mapUpdate)
            {
                UpdateMap(true);
            }
        }

        public override void Update()
        {
            // Do stuff every new frame
        }

        public void UpdateMap(bool invertLoading = false)
        {
            if (!isActive)
            {
                return;
            }

            List<Entity> list1 , list2;

            list1 = DisableElements;
            list2 = EnableElements;

            if (invertLoading)
            {
                list1 = EnableElements;
                list2 = DisableElements;
            }
            else
            {
                PlaySound();
            }

            foreach (Entity entity in list1)
            {
                if (entity.Get<StaticColliderComponent>() != null)
                {
                    entity.Get<StaticColliderComponent>().Enabled = false;
                }

                if (entity.Get<SpriteComponent>() != null)
                {
                    entity.Get<SpriteComponent>().Enabled = false;
                }

                if (entity.Get<ModelComponent>() != null)
                {
                    entity.Get<ModelComponent>().Enabled = false;
                }

                if (entity.Get<TriggerEventByDistance>() != null)
                {
                    entity.Get<TriggerEventByDistance>().isActive = false;
                }

                if (entity.Get<TriggerEventManually>() != null)
                {
                    entity.Get<TriggerEventManually>().isActive = false;
                }

                IEnumerable<Entity> entities = entity.GetChildren();

                if (!entities.Any())
                {
                    continue;
                }

                foreach (Entity child in entities)
                {
                    if (child.Get<SpriteComponent>() != null)
                    {
                        child.Get<SpriteComponent>().Enabled = false;
                    }

                    if (child.Get<ModelComponent>() != null)
                    {
                        child.Get<ModelComponent>().Enabled = false;
                    }
                }
            }

            foreach (Entity entity in list2)
            {
                if (entity.Get<StaticColliderComponent>() != null)
                {
                    entity.Get<StaticColliderComponent>().Enabled = true;
                }

                if (entity.Get<SpriteComponent>() != null)
                {
                    entity.Get<SpriteComponent>().Enabled = true;
                }

                if (entity.Get<ModelComponent>() != null)
                {
                    entity.Get<ModelComponent>().Enabled = true;
                }

                if (entity.Get<TriggerEventByDistance>() != null)
                {
                    entity.Get<TriggerEventByDistance>().isActive = true;
                }

                if (entity.Get<TriggerEventManually>() != null)
                {
                    entity.Get<TriggerEventManually>().isActive = true;
                }

                IEnumerable<Entity> entities = entity.GetChildren();

                if (!entities.Any())
                {
                    continue;
                }

                foreach (Entity child in entities)
                {
                    if (child.Get<SpriteComponent>() != null)
                    {
                        child.Get<SpriteComponent>().Enabled = true;
                    }

                    if (child.Get<ModelComponent>() != null)
                    {
                        child.Get<ModelComponent>().Enabled = true;
                    }
                }
            }
        }

        public void PlaySound()
        {
            if (Sound != null)
            {
                var instance = Sound.CreateInstance();
                instance.Play();
            }
        }

        public void ChangeScene ()
        {

        }
    }
}
