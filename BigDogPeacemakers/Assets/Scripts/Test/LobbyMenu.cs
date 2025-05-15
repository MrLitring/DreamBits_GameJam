using UnityEngine;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{
    public InputField joinCodeInput;
    public Text joinCodeDisplay;
    public Button hostButton;
    public Button joinButton;

    private void Start()
    {
        hostButton.onClick.AddListener(OnHostClicked);
        joinButton.onClick.AddListener(OnJoinClicked);
    }

    private async void OnHostClicked()
    {
        string joinCode = await NetworkManagerRelay.Instance.CreateRelay(2); // ��������, 2 ������
        if (joinCode != null && joinCodeDisplay != null)
        {
            joinCodeDisplay.text = $"JOIN CODE: {joinCode}";
        }
    }

    private async void OnJoinClicked()
    {
        string code = joinCodeInput.text;
        if (!string.IsNullOrEmpty(code))
        {
            Debug.Log("���: "+ code);
            await NetworkManagerRelay.Instance.JoinRelay(code);
        }
    }
}
