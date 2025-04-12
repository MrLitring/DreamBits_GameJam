using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour, INeedToLoad
{
    public static SceneLoadConfig NextSceneLoad = null;


    [Header("�����")]
    [Tooltip("����� ��������\n (None - ����������)")]
    public SceneEnums.GameScene screenLoadingName = SceneEnums.GameScene.None;
    [Tooltip("��������� �����")]
    public SceneEnums.GameScene SceneName = SceneEnums.GameScene.None;

    [Space(10)]
    [Header("��������")]
    [Tooltip("�������� ��������")]
    public Animator Transition;
    [Tooltip("������������ �������� ��������")]
    public float TransitionTimeDelay = 1f;
    [Tooltip("����������� ����� ��������")]
    public float minLoadTime = 2f;

    private AsyncOperation loadingOperation;
    private Coroutine loadingRoutine;
    private float loadStartTime;

    void INeedToLoad.NeedToFirstLoad()
    {
        if (Transition != null && !Transition.gameObject.activeSelf)
            Transition.gameObject.SetActive(true);

        if (NextSceneLoad != null)
        {
            ApplySceneConfig();
            StartLoadScene();
        }
    }

    public void StartLoadScene()
    {
        if (loadingRoutine != null)
        {
            StopCoroutine(loadingRoutine);
        }

        loadingRoutine = StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        // 1. �������� ������� �����
        if (SceneName == SceneEnums.GameScene.None)
        {
            Debug.LogError($"{name}: Target scene not specified!");
            yield break;
        }

        // 2. ������ �������� ��������
        if (Transition != null)
        {
            Transition.SetTrigger("Start");
            yield return new WaitForSeconds(TransitionTimeDelay);
        }

        // 3. �������� ������������� ������������ ������
        if (screenLoadingName != SceneEnums.GameScene.None)
        {
            NextSceneLoad = new SceneLoadConfig(SceneName, false, false);
            SceneManager.LoadScene((int)screenLoadingName);
            yield break;
        }

        // 4. ����������� ��������
        loadStartTime = Time.time;
        loadingOperation = SceneManager.LoadSceneAsync((int)SceneName);
        loadingOperation.allowSceneActivation = false;

        // 5. �������� ��������
        while (!loadingOperation.isDone)
        {
            if (loadingOperation.progress >= 0.9f)
            {
                // ��������� ����������� ����� ��������
                float elapsedTime = Time.time - loadStartTime;
                if (elapsedTime >= minLoadTime)
                {
                    // �������������� ��������� �����
                    loadingOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    private void ApplySceneConfig()
    {
        if (NextSceneLoad == null) return;

        SceneName = NextSceneLoad.SceneName;
        // ������ �������� isWaitingClick ��� ��� ��� ������ �� �����
    }

    void OnDestroy()
    {
        if (loadingRoutine != null)
        {
            StopCoroutine(loadingRoutine);
        }
    }

}
