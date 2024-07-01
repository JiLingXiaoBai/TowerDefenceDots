using Unity.Entities;
using Unity.Entities.Serialization;

namespace Components
{
    public struct PrefabReferenceComponent : IComponentData
    {
        public EntityPrefabReference Value;
    }

    public struct CleanupPrefabReferenceComponent : ICleanupComponentData
    {
        public Entity PrefabToUnload;
    }
}