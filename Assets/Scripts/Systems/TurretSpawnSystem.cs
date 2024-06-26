using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public partial struct TurretSpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TurretPrefabRef>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var turretComponent in SystemAPI.Query<RefRO<TurretPrefabRef>>())
            {
                var turretRO = turretComponent.ValueRO;
                var turretEnt = ecb.Instantiate(turretRO.TurretPrefab);
                
                var trans = new LocalTransform()
                {
                    Rotation = quaternion.RotateY((float)(90f * math.TORADIANS_DBL)),
                    Scale = 1f,
                    Position = float3.zero,
                };
                ecb.SetComponent(turretEnt, trans);
                
                var tbTurret = TDRoot.Tables.TbTurret.GetOrDefault(turretRO.TurretId);
                ecb.AddComponent(turretEnt, new TurretComponent()
                {
                    BulletId = tbTurret.BulletId,
                    FireRate = tbTurret.FireRate
                });
            }

            ecb.Playback(state.EntityManager);
            state.Enabled = false;
        }
    }
}