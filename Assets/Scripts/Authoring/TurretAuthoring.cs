using Unity.Entities;
using UnityEngine;
using Components;
namespace Authoring
{
    public class TurretAuthoring : MonoBehaviour
    {
        private class TurretBaker : Baker<TurretAuthoring>
        {
            public override void Bake(TurretAuthoring authoring)
            {
                var ent = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(ent, new TurretComponent());
            }
        }
    }
}