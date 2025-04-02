using UnityEngine;

public class SceneLoadConfig
{
    public SceneEnums.GameScene SceneName { get; private set; }
    public bool isLoadingWithoutWaiting { get; private set; }
    public bool isWaitingClick { get; private set; }


    public SceneLoadConfig(SceneEnums.GameScene gameScene, bool isWaitingClick, bool isLoadingWithoutWaiting = true)
    {
        this.SceneName = gameScene;
        this.isWaitingClick = isWaitingClick;
        this.isLoadingWithoutWaiting = isLoadingWithoutWaiting;
    }
}
