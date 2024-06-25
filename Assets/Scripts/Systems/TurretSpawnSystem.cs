using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

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
                var spawnerRO = spawnerComponent.ValueRO;
                var turretEnt = state.EntityManager.Instantiate(spawnerRO.TurretEntity);
                
                var trans = state.EntityManager.GetComponentData<LocalTransform>(turretEnt);
                trans.Rotation = quaternion.RotateY((float)(90f * math.TORADIANS_DBL));
                state.EntityManager.SetComponentData(turretEnt, trans);

                var tbTurret = TDRoot.Tables.TbTurret.GetOrDefault(spawnerRO.TurretId);
                var turret = state.EntityManager.GetComponentData<TurretComponent>(turretEnt);
                turret.BulletId = tbTurret.BulletId;
                turret.FireRate = tbTurret.FireRate;
                state.EntityManager.SetComponentData(turretEnt, turret);
            }
        }
    }
}