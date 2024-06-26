using Unity.Entities;
using UnityEngine;
using Components;
using YooAsset;

namespace Authoring
{
    public class TurretSpawnerAuthoring : MonoBehaviour
    {
        private class TurretSpawnerBaker : Baker<TurretSpawnerAuthoring>
        {
            public override void Bake(TurretSpawnerAuthoring authoring)
            {
                foreach (var tbTurret in TDRoot.Tables.TbTurret.DataList)
                {
                    var turretPrefab = CreateAdditionalEntity(TransformUsageFlags.Dynamic);
                    var handle = YooAssets.LoadAssetSync<GameObject>(tbTurret.Prefab);
                    var prefabEnt = GetEntity((GameObject)handle.AssetObject, TransformUsageFlags.Dynamic);
                    
                    AddComponent(turretPrefab, new TurretPrefabRef
                    {
                        TurretId = tbTurret.Id,
                        TurretPrefab = prefabEnt
                    });
                }
            }
        }
    }
}