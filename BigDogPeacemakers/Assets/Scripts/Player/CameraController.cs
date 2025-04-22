using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineFollow cinemachineFollow;
    CinemachineCamera cinemachineCamera;
    PlayerInputHandler inputHandler;

    public float offsetX = 5.5f;
    public float offsetY = 2.16f;
    public float orthgraphic = 5.87f;
    public float speedCamera = 0.1f;

    private void Start()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
        cinemachineFollow = GetComponent<CinemachineFollow>();
        inputHandler = cinemachineCamera.Target.TrackingTarget.gameObject.GetComponent<PlayerInputHandler>();
        cinemachineFollow.FollowOffset.x = offsetX;
        cinemachineFollow.FollowOffset.y = offsetY;
        cinemachineCamera.Lens.OrthographicSize = orthgraphic;

    }
    private void LateUpdate()
    {
        if (inputHandler.GetMove().x > 0)
        {
            cinemachineFollow.FollowOffset.x = Mathf.Min(cinemachineFollow.FollowOffset.x + speedCamera, offsetX) ;
        }else if(inputHandler.GetMove().x < 0)
        {
            cinemachineFollow.FollowOffset.x = Mathf.Max(cinemachineFollow.FollowOffset.x - speedCamera, -offsetX);
        }
    }


}
