using System.Collections;
using UnityEngine;
using YooAsset;

public class Boot : MonoBehaviour
{
    
    private const string PackageName = "DefaultPackage";
    private const EDefaultBuildPipeline BuildPipeline = EDefaultBuildPipeline.BuiltinBuildPipeline;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        YooAssets.Initialize();

        yield return InitPackage();

        var package = YooAssets.TryGetPackage(PackageName);
        YooAssets.SetDefaultPackage(package);

        yield return YooAssets.LoadSceneAsync("SampleScene");
    }


    private static IEnumerator InitPackage()
    {
        // ReSharper disable once ConvertToConstant.Local
        var isEditor = true;
#if !UNITY_EDITOR
        isEditor = false;
#endif
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        var playMode = isEditor ? EPlayMode.EditorSimulateMode : EPlayMode.OfflinePlayMode;
        var package = YooAssets.CreatePackage(PackageName);

        InitializationOperation initializationOperation = null;
        switch (playMode)
        {
            case EPlayMode.EditorSimulateMode:
            {
                var createParameters = new EditorSimulateModeParameters
                {
                    SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(BuildPipeline.ToString(), PackageName)
                };
                initializationOperation = package.InitializeAsync(createParameters);
                break;
            }
            case EPlayMode.OfflinePlayMode:
            {
                var createParameters = new OfflinePlayModeParameters();
                initializationOperation = package.InitializeAsync(createParameters);
                break;
            }
            // ReSharper disable once UnreachableSwitchCaseDueToIntegerAnalysis
            case EPlayMode.HostPlayMode:
            // ReSharper disable once UnreachableSwitchCaseDueToIntegerAnalysis
            case EPlayMode.WebPlayMode:
            // ReSharper disable once UnreachableSwitchCaseDueToIntegerAnalysis
            default:
                break;
        }

        yield return initializationOperation;

        if (initializationOperation == null)
        {
            Debug.LogError("package initialize error due to EPlayMode");
        }else if (initializationOperation.Status != EOperationStatus.Succeed)
        {
            Debug.LogError($"{initializationOperation.Error}");
        }
        else
        {
            var version = initializationOperation.PackageVersion;
            Debug.Log($"Init resource package version : {version}");
        }
    }

}
