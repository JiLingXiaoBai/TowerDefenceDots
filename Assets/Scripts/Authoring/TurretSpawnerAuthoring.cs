using Unity.Entities;
using UnityEngine;
using Components;

namespace Authoring
{
    public class TurretSpawnerAuthoring : MonoBehaviour
    {
        public int turretId;
        private class TurretSpawnerBaker : Baker<TurretSpawnerAuthoring>
        {
            public override void Bake(TurretSpawnerAuthoring authoring)
            {
                var ent = GetEntity(TransformUsageFlags.None);
                AddComponent(ent, new TurretSpawnerComponent()
                {
                    TurretId = authoring.turretId
                });
            }
        }
    }
}