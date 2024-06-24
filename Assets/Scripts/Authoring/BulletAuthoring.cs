using Unity.Entities;
using UnityEngine;
using Components;
namespace Authoring
{
    public class BulletAuthoring : MonoBehaviour
    {
        private class BulletBaker : Baker<BulletAuthoring>
        {
            public override void Bake(BulletAuthoring authoring)
            {
                var ent = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(ent, new BulletComponent());
            }
        }
    }
}