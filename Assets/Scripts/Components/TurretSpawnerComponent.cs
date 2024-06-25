using Unity.Entities;

namespace Components
{
    public struct TurretSpawnerComponent : IComponentData
    {
        public int TurretId;
        public Entity TurretEntity;
    }
}