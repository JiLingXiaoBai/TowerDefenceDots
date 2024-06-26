using Components;
using Unity.Entities;

namespace Systems
{
    public partial struct TurretShootingSystem : ISystem
    {

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TurretComponent>();
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
                if (shoot)
                {
                    UnityEngine.Debug.Log("Shooting Bullet" + turretRO.BulletId);
                }
            }
        }
    }
}