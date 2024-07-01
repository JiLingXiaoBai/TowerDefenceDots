using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Scenes;

namespace Systems
{
    [RequireMatchingQueriesForUpdate]
    public partial struct LoadPrefabSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (prefabRef, entity) in SystemAPI.Query<RefRO<PrefabReferenceComponent>>()
                         .WithNone<RequestEntityPrefabLoaded>().WithEntityAccess())
            {
                ecb.AddComponent(entity, new RequestEntityPrefabLoaded
                {
                    Prefab = prefabRef.ValueRO.Value
                });
            }

            foreach (var (loadedPrefab, entity) in SystemAPI.Query<RefRO<PrefabLoadResult>>()
                         .WithAll<PrefabReferenceComponent>().WithEntityAccess())
            {
                var prefabEntity = ecb.Instantiate(loadedPrefab.ValueRO.PrefabRoot);
                ecb.AddComponent(entity, new CleanupPrefabReferenceComponent()
                {
                    PrefabToUnload = prefabEntity
                });
                ecb.RemoveComponent<PrefabReferenceComponent>(entity);
                ecb.RemoveComponent<RequestEntityPrefabLoaded>(entity);
            }

            // ecb.Playback(state.EntityManager);

            foreach (var (prefabRef, entity) in SystemAPI.Query<RefRO<CleanupPrefabReferenceComponent>>()
                         .WithNone<SceneTag>().WithEntityAccess())
            {
                ecb.DestroyEntity(prefabRef.ValueRO.PrefabToUnload);
                ecb.RemoveComponent<CleanupPrefabReferenceComponent>(entity);
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}