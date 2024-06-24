using UnityEngine;
using YooAsset;

public class ConfigTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var item = TDRoot.Tables.TbItem.GetOrDefault(10000);
        Debug.Log(item.Name);

        var handle = YooAssets.LoadAssetSync<GameObject>("Assets/GameRes/Cube");
        Instantiate(handle.AssetObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    
}
