using UnityEngine;
using YooAsset;

public class Test : MonoBehaviour
{
    private void Start()
    {
        var tbTurret = TDRoot.Tables.TbTurret.GetOrDefault(1);
        var handle = YooAssets.LoadAssetSync<GameObject>(tbTurret.Prefab);
        Instantiate(handle.AssetObject);
    }
}