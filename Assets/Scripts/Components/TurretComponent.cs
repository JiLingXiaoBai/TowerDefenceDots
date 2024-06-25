using Unity.Entities;

namespace Components
{
    public struct TurretComponent : IComponentData
    {
        public int BulletId;
        public float FireRate;
        public float AccumulatedTime;
    }
}