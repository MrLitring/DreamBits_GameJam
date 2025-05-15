using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LobbyManager : NetworkBehaviour
{
    public NetworkList<ulong> playerList = new NetworkList<ulong>();

    private void Awake()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true;
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private void Start()
    {
        if (IsHost)
        {
            ulong hostClientId = NetworkManager.Singleton.LocalClientId;
            playerList.Add(hostClientId);
        }
    }
    private void OnClientConnected(ulong clientId)
    {
        if (IsServer)
        {
            Debug.Log($"[Server] ����������� ������ {clientId}");
            playerList.Add(clientId);
        }
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (IsServer)
        {
            Debug.Log($"[Server] ���������� ������ {clientId}");
            playerList.Remove(clientId);
        }
    }


    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        //  �������� ������
        string clientString = System.Text.Encoding.UTF8.GetString(request.Payload);

        Debug.Log($"[Server] ����������� � �������: {clientString}");

        //  ����� ���� ������ � �������� ������
        bool isApproved = clientString == "1111"; // ������� ������ ����������

        //  ��������� ������
        response.Approved = isApproved;
        response.CreatePlayerObject = true;
        response.PlayerPrefabHash = null; // ����� ������� ���������� ������ ������
        response.Position = null;
        response.Rotation = null;
        response.Pending = false; // true � ���� ������ �������� �����
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            NetworkManager.Singleton.Shutdown();
            SceneManager.LoadScene(0);
        }
    }
}
