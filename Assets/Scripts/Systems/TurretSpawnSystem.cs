using Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

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
            if (_spawnCount > 1)
                return;
            _spawnCount++;

            foreach (var spawnerComponent in SystemAPI.Query<RefRO<TurretSpawnerComponent>>())
            {
                var turretId = spawnerComponent.ValueRO.TurretId;
                Debug.Log("turretId " + turretId);
                var tbTurret = TDRoot.Tables.TbTurret[turretId];
                Debug.Log("turretPrefab " + tbTurret.Prefab);
            }
        }
    }
}