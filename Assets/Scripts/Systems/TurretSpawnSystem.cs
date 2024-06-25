using Components;
using Unity.Collections;
using Unity.Entities;

namespace Systems
{
    public partial struct TurretSpawnSystem : ISystem
    {
        private int _spawnCount;
        private EntityQuery _entityQuery;

        public void OnCreate(ref SystemState state)
        {
            var queryBuilder = new EntityQueryBuilder(Allocator.Temp).WithAll<TurretSpawnerComponent>();
            _entityQuery = state.GetEntityQuery(queryBuilder);
            state.RequireForUpdate(_entityQuery);
        }

        public void OnUpdate(ref SystemState state)
        {
            if (_spawnCount > 0)
                return;
            _spawnCount++;

            foreach (var spawnerComponent in SystemAPI.Query<RefRO<TurretSpawnerComponent>>())
            {
                state.EntityManager.Instantiate(spawnerComponent.ValueRO.TurretEntity);
            }
        }
    }
}