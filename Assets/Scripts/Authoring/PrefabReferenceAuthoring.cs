using Components;
using Unity.Entities;
using Unity.Entities.Serialization;
using UnityEngine;

namespace Authoring
{
    public class PrefabReferenceAuthoring : MonoBehaviour
    {
        public GameObject prefab;

        private class PrefabReferenceBaker : Baker<PrefabReferenceAuthoring>
        {
            public override void Bake(PrefabReferenceAuthoring authoring)
            {
                GetEntity(authoring.prefab, TransformUsageFlags.Dynamic);
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new PrefabReferenceComponent
                {
                    Value = new EntityPrefabReference(authoring.prefab)
                });
            }
        }
    }
}