using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, INeedToLoad
{

    public static SceneLoadConfig NextSceneLoad = null;

    // ��������� ������������ ����
    //
    [Tooltip("�������� ����� ��������\n ���� None - ��� ������������ ������")]
    public SceneEnums.ScreenLoading screenLoadingName = SceneEnums.ScreenLoading.None;

    [Space(20)]
    [Tooltip("��������� ��������� �����")]
    public SceneEnums.GameScene SceneName = SceneEnums.GameScene.None;
    [Tooltip("��������� ��������� ����� �� �������?")]
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
            Debug.LogError($"{this.name} - ����� �� ���������!");
            return;
        }

        if (SceneName != SceneEnums.GameScene.None)
        {
            if (screenLoadingName != SceneEnums.ScreenLoading.None)
            {
                Debug.Log("�������� �� ����������� �����");
                NextSceneLoad = GenerateSceneConfig();
                SceneManager.LoadScene((int)screenLoadingName);
                return;
            }
        }

        Debug.Log($"�������� ������� �����: {SceneName}");
        AsyncLoadScene();
    }

    private SceneLoadConfig GenerateSceneConfig()
    {
        return new SceneLoadConfig(SceneName, isWaitingClick, true);
    }

    private void ApplySceneConfig()
    {
        if (NextSceneLoad == null) return;
        Debug.Log($"��������� ������������ = SceneName = {NextSceneLoad.SceneName}, isWaitingClick = {NextSceneLoad.isWaitingClick}, isLoadingWithoutWaiting = {NextSceneLoad.isLoadingWithoutWaiting}");

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
        Debug.Log($"������� �������� �����: {SceneName}");

        
        
        operation = SceneManager.LoadSceneAsync((int)SceneName);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            Debug.Log("�������� �������� �����: " + operation.progress);
            yield return null;
        }

        Debug.Log($"�����: {SceneName} - ������! ��� ����� LoadScene");
        isSceneReady = true;
        coroutine = null;
    }

}
