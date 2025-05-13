using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;

public class CPlayerSpawner : NetworkBehaviour
{
    public Vector2 startPos;
    public GameObject playerPrefab;
    public float distanceBetweenX;
    public ColorsOfSpheres colorsOfSpheres;

    UIDocument uiDocument;
    VisualElement root;
    Label label;

    float timer;
    float cooldown = 0.1f;

    GameObject myPlayer;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
    }
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            SpawnPlayers();
        }
        CreateUIChangeColor();
    }

    void SpawnPlayers()
    { 
        int n = 0;
        foreach (var clients in NetworkManager.ConnectedClientsIds)
        {
            GameObject playerObj; 
            if (n % 2 == 0) {
                playerObj = Instantiate(playerPrefab, (Vector3)(new Vector2(startPos.x - distanceBetweenX * n, startPos.y)), Quaternion.identity);
            }
            else
            {
                playerObj = Instantiate(playerPrefab, (Vector3)(new Vector2(- (startPos.x - distanceBetweenX * (n-1)), startPos.y)), new Quaternion(Quaternion.identity.x, 180, Quaternion.identity.z, Quaternion.identity.w));
            }
            playerObj.GetComponent<NetworkObject>().SpawnAsPlayerObject(clients);
            n++;
        }

    }

    void CreateUIChangeColor()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject player = null;
        foreach (var p in players)
        {
            if (p.GetComponent<NetworkObject>().IsOwner)
            {
                player = p;
                myPlayer = p;
                break;
            }
        }
        if (player != null)
        {
            label = new Label();
            label.text = "Нажмите Вниз для смены цвета";
            label.style.position = Position.Absolute;
            label.style.fontSize = 25;
            label.style.borderLeftWidth = 10f;
            label.style.color = Color.white;

            Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            Vector2 dot = cam.WorldToScreenPoint(new Vector2(player.transform.position.x - 2.8f, player.transform.position.y - 3));

            //ChangeLabelColor(label, p.GetComponent<PlayerData>().SphereColor);
            label.style.left = dot.x;
            label.style.top = dot.y;

            root.Add(label);
        }
    }

    void ChangeLabelColor(Label label, UnityEngine.Color color)
    {
        label.style.color = color;

        label.style.borderLeftColor = color;
        //label.style.borderLeftColor = new UnityEngine.Color(255 - color.r * 255, 255 - color.g * 255, 255 - color.b * 255, 1);
    }

    private void Update()
    {
        
        
        
        if (Input.GetKey(KeyCode.E) && timer <= 0)
        {
            PlayerData playerData = myPlayer.GetComponent<PlayerData>();
            Color currentColor = playerData.SphereColor;
            int n = colorsOfSpheres.colors.IndexOf(currentColor);
            Color nextColor = n < colorsOfSpheres.colors.Count - 1 ? colorsOfSpheres.colors[n + 1] : colorsOfSpheres.colors[0];
            NextColorServerRpc(myPlayer.GetComponent<NetworkObject>().NetworkObjectId, nextColor);
            ChangeLabelColor(label, nextColor);
            timer = cooldown;
        }
        if (timer > 0) timer -= Time.deltaTime;
    }

    [ServerRpc(RequireOwnership =false)]
    void NextColorServerRpc(ulong networkObjectId , Color nextColor)
    {
        NextColorClientRpc(networkObjectId, nextColor);
    }

    [ClientRpc(RequireOwnership =false)]
    void NextColorClientRpc(ulong networkObjectId, Color nextColor)
    {

        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var netObj))
        {
            netObj.GetComponent<PlayerData>().ChangeColor(nextColor);
        }
    }
}
