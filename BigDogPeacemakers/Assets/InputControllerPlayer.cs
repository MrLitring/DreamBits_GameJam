using UnityEngine;
using UnityEngine.InputSystem;

public class InputControllerPlayer : MonoBehaviour
{
    PlayerInput thisPlayerInput;

    private void Awake()
    {
        thisPlayerInput = GetComponent<PlayerInput>();
        var device = thisPlayerInput.devices[0];

        if (device is Gamepad)
        {
            //thisPlayerInput.SwitchCurrentActionMap("Gamepad");
            thisPlayerInput.defaultActionMap = "Gamepad";
            //thisPlayerInput.defaultControlScheme = "Gamepad";
        }
        else if (device is Keyboard)
        {
            //thisPlayerInput.SwitchCurrentActionMap("Keyboard");
            thisPlayerInput.defaultActionMap = "Keyboard";
            //thisPlayerInput.defaultControlScheme = "Keyboard&Mouse";
        }
        else
        {
            Debug.LogError("Неизвестное устройство - экшн мап не настроен корректно");
        }
        thisPlayerInput.actions.Enable();

    }
    public void OnPlayerJoin(PlayerInput playerInput)
    {
        

    }

}
