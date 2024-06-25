using System.Linq;
using Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

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
            foreach (var turretComponent in SystemAPI.Query<RefRW<TurretComponent>>())
            {
                var turretRO = turretComponent.ValueRO;
                var shoot = false;
                if (turretRO.AccumulatedTime >= turretRO.FireRate)
                {
                    turretComponent.ValueRW.AccumulatedTime -= turretRO.FireRate;
                    shoot = true;
                }
                turretComponent.ValueRW.AccumulatedTime += SystemAPI.Time.DeltaTime;
                // if (shoot)
                // {
                //     UnityEngine.Debug.Log("Shooting");
                // }
            }
        }
    }
}