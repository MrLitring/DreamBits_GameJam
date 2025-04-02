using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    [Tooltip("������ ��������, ��������� �������������� ��������, ������, ���� ���� �������� INeedLoad")]
    public List<MonoBehaviour> listOfInitialLaunch = new List<MonoBehaviour>();
    

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
    }
}
