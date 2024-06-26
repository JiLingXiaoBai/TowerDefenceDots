using Unity.Entities;

namespace Components
{
    public struct TurretPrefabRef : IComponentData
    {
        public int TurretId;
        public Entity TurretPrefab;
    }

    public struct TurretComponent : IComponentData
    {
        public int BulletId;
        public float FireRate;
        public float AccumulatedTime;
    }
    
}