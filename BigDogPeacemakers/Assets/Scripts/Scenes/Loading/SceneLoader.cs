using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, INeedToLoad
{

    public static SceneLoadConfig NextSceneLoad = null;

    // Публичные настраиваемы поля
    //
    [Tooltip("Показать экран загрузки\n Если None - без загрузочного экрана")]
    public SceneEnums.ScreenLoading screenLoadingName = SceneEnums.ScreenLoading.None;

    [Space(20)]
    [Tooltip("Загрузить следующую сцену")]
    public SceneEnums.GameScene SceneName = SceneEnums.GameScene.None;
    [Tooltip("Загрузить следующую сцену по нажатию?")]
    public bool isWaitingClick = false;


    private bool isSceneReady = false;
    private AsyncOperation operation = null;
    private Coroutine coroutine { get; set; }


    void INeedToLoad.NeedToFirstLoad()
    {
        ApplySceneConfig();
        bool isWait = NextSceneLoad.isLoadingWithoutWaiting;
        NextSceneLoad = null;

        if (isWait) StartLoadScene();
    }


    public void StartLoadScene()
    {
        Debug.Log($"StartLoadScene: screenLoadingName = {screenLoadingName}, SceneName = {SceneName}");

        if (isSceneReady)
        {
            if (operation != null)
                operation.allowSceneActivation = true;
            return;
        }

        if (SceneName == SceneEnums.GameScene.None)
        {
            Debug.LogError($"{this.name} - Сцена не назначена!");
            return;
        }

        if (SceneName != SceneEnums.GameScene.None)
        {
            if (screenLoadingName != SceneEnums.ScreenLoading.None)
            {
                Debug.Log("Перехожу на загрузочный экран");
                NextSceneLoad = GenerateSceneConfig();
                SceneManager.LoadScene((int)screenLoadingName);
                return;
            }
        }

        Debug.Log($"Загружаю целевую сцену: {SceneName}");
        AsyncLoadScene();
    }

    private SceneLoadConfig GenerateSceneConfig()
    {
        return new SceneLoadConfig(SceneName, isWaitingClick, true);
    }

    private void ApplySceneConfig()
    {
        if (NextSceneLoad == null) return;
        Debug.Log($"Следующая конфигурация = SceneName = {NextSceneLoad.SceneName}, isWaitingClick = {NextSceneLoad.isWaitingClick}, isLoadingWithoutWaiting = {NextSceneLoad.isLoadingWithoutWaiting}");

        this.SceneName = NextSceneLoad.SceneName;
        this.isWaitingClick = NextSceneLoad.isWaitingClick;
    }

    private void AsyncLoadScene()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        else
            coroutine = StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        Debug.Log($"Начинаю загрузку сцены: {SceneName}");

        
        
        operation = SceneManager.LoadSceneAsync((int)SceneName);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            Debug.Log("Прогресс загрузки сцены: " + operation.progress);
            yield return null;
        }

        Debug.Log($"Сцена: {SceneName} - готова! Жду вызов LoadScene");
        isSceneReady = true;
        coroutine = null;
    }

}
