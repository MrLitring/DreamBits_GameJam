using UnityEngine;
using Unity.Netcode;

public class TestRpc : NetworkBehaviour
{
    string text;
    public override void OnNetworkSpawn()
    {
        if (IsClient)
        {
            text = "��� ������";
        }
        if (IsServer)
        {
            text = "��� ������";
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(text);
    }
}
