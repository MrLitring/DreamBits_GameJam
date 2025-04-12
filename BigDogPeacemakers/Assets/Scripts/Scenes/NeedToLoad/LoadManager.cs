using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    [Tooltip("Список объектов, требующих первоначальной загрузки, Только, если есть интерфей INeedLoad")]
    public List<MonoBehaviour> listOfInitialLaunch = new List<MonoBehaviour>();

    [Tooltip("Список объектов, требующих включения на сцене, если отключены")]
    public List<Transform> ListSelfActive = new List<Transform>();


    public void Awake()
    {
        Initilization();
    }


    private void Initilization()
    {
        foreach (var item in listOfInitialLaunch)
        {
            if(item is INeedToLoad elem)
                elem.NeedToFirstLoad();
        }

        foreach(Transform elem in ListSelfActive)
        {
            GameObject item = elem.gameObject;

            if (!item.activeSelf) item.SetActive(true);
        }
    }
}
