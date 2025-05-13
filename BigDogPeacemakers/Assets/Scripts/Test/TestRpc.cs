using UnityEngine;
using Unity.Netcode;

public class TestRpc : NetworkBehaviour
{
    string text;
    public override void OnNetworkSpawn()
    {
        if (IsClient)
        {
            text = "Это клиент";
        }
        if (IsServer)
        {
            text = "Это сервер";
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(text);
    }
}
