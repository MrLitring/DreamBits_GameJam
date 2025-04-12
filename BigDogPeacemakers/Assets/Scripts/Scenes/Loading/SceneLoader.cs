using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour, INeedToLoad
{
    public static SceneLoadConfig NextSceneLoad = null;


    [Header("Экран")]
    [Tooltip("Экран загрузки\n (None - пропустить)")]
    public SceneEnums.GameScene screenLoadingName = SceneEnums.GameScene.None;
    [Tooltip("Следующая сцена")]
    public SceneEnums.GameScene SceneName = SceneEnums.GameScene.None;

    [Space(10)]
    [Header("Анимации")]
    [Tooltip("Аниматор перехода")]
    public Animator Transition;
    [Tooltip("Длительность анимации перехода")]
    public float TransitionTimeDelay = 1f;
    [Tooltip("Минимальное время загрузки")]
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
        // 1. Проверка целевой сцены
        if (SceneName == SceneEnums.GameScene.None)
        {
            Debug.LogError($"{name}: Target scene not specified!");
            yield break;
        }

        // 2. Запуск анимации перехода
        if (Transition != null)
        {
            Transition.SetTrigger("Start");
            yield return new WaitForSeconds(TransitionTimeDelay);
        }

        // 3. Проверка необходимости загрузочного экрана
        if (screenLoadingName != SceneEnums.GameScene.None)
        {
            NextSceneLoad = new SceneLoadConfig(SceneName, false, false);
            SceneManager.LoadScene((int)screenLoadingName);
            yield break;
        }

        // 4. Асинхронная загрузка
        loadStartTime = Time.time;
        loadingOperation = SceneManager.LoadSceneAsync((int)SceneName);
        loadingOperation.allowSceneActivation = false;

        // 5. Ожидание загрузки
        while (!loadingOperation.isDone)
        {
            if (loadingOperation.progress >= 0.9f)
            {
                // Проверяем минимальное время загрузки
                float elapsedTime = Time.time - loadStartTime;
                if (elapsedTime >= minLoadTime)
                {
                    // Автоматическая активация сцены
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
        // Убрана проверка isWaitingClick так как она больше не нужна
    }

    void OnDestroy()
    {
        if (loadingRoutine != null)
        {
            StopCoroutine(loadingRoutine);
        }
    }

}
