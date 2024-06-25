using Components;
using Unity.Collections;
using Unity.Entities;

namespace Systems
{
    public partial struct TurretShootingSystem : ISystem
    {
        private EntityQuery _entityQuery;

        public void OnCreate(ref SystemState state)
        {
            var queryBuilder = new EntityQueryBuilder(Allocator.Temp).WithAll<TurretComponent>();
            _entityQuery = state.GetEntityQuery(queryBuilder);
            state.RequireForUpdate(_entityQuery);
        }

        public void OnUpdate(ref SystemState state)
        {
            UnityEngine.Debug.Log("ShootingSystem");
            foreach (var turretComponent in SystemAPI.Query<RefRW<TurretComponent>>())
            {
                var turretRW = turretComponent.ValueRW;
                var turretRO = turretComponent.ValueRO;
                var shoot = false;
                if (turretRW.AccumulatedTime >= turretRO.FireRate)
                {
                    turretRW.AccumulatedTime -= turretRO.FireRate;
                    shoot = true;
                }
                turretRW.AccumulatedTime += SystemAPI.Time.DeltaTime;

                if (shoot)
                {
                    UnityEngine.Debug.Log("Shooting");
                }
            }
        }
    }
}