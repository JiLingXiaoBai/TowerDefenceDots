using Unity.Entities;
using UnityEngine;
using Components;
using YooAsset;

namespace Authoring
{
    public class TurretSpawnerAuthoring : MonoBehaviour
    {
        public int turretId;
        private class TurretSpawnerBaker : Baker<TurretSpawnerAuthoring>
        {
            public override void Bake(TurretSpawnerAuthoring authoring)
            {
                var tbTurret = TDRoot.Tables.TbTurret[authoring.turretId];
                var handle = YooAssets.LoadAssetSync<GameObject>(tbTurret.Prefab);
                var spawnerEnt = GetEntity(TransformUsageFlags.None);
                var turretEnt = GetEntity((GameObject)handle.AssetObject, TransformUsageFlags.Dynamic);
                AddComponent(spawnerEnt, new TurretSpawnerComponent
                {
                    TurretId = authoring.turretId,
                    TurretEntity = turretEnt
                });
            }
        }
    }
}