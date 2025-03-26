using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;
public class CameraController : MonoBehaviour
{
    public CinemachineCamera virtualCamera;
    public Transform player;
    public Rigidbody2D playerRb;
    public Collider2D cameraBounds;

    public float followSpeed = 3f; // �������� ����������
    public float expandSpeed = 1.5f; // �������� ���������� ������
    public float expandedViewSize = 8f; // ����������� ������ ������
    public float defaultViewSize = 5f; // ������� ������ ������
    public float holdTimeToExpand = 1.5f; // �����, ����� ������� ���������� �����
    public float edgeThreshold = 0.2f; // ������� ������� (0.2 = 20% �� ������)
    public float cameraMoveThreshold = 0.4f; // ��������� ������ ����� ����� ����� �� ������ ������

    private CinemachinePositionComposer framing;
    private float holdTime = 0f;
    private bool isExpanding = false;
    private Vector3 cameraTargetOffset = Vector3.zero;
    private float lastPlayerX; // ��� ����������� ����������� ��������

    void Start()
    {
        framing = virtualCamera.GetComponent<CinemachinePositionComposer>();
        lastPlayerX = player.position.x;

    }

    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(player.position);
        bool touchingEdge = viewPos.x < edgeThreshold || viewPos.x > 1 - edgeThreshold ||
                            viewPos.y < edgeThreshold || viewPos.y > 1 - edgeThreshold;

        float playerDirection = Mathf.Sign(player.position.x - lastPlayerX);
        lastPlayerX = player.position.x;

        if (touchingEdge)
        {
            holdTime += Time.deltaTime;

            if (holdTime >= holdTimeToExpand)
            {
                ExpandView();
            }
            else
            {
                SmoothMoveCamera(playerDirection);
            }
        }
        else
        {
            holdTime = 0f;
            ResetView();
        }
    }

    void SmoothMoveCamera(float playerDirection)
    {
        float targetXOffset = cameraMoveThreshold * playerDirection;

        if (Mathf.Abs(cameraTargetOffset.x - targetXOffset) > 0.01f)
        {
            cameraTargetOffset.x = Mathf.Lerp(cameraTargetOffset.x, targetXOffset, followSpeed * Time.deltaTime);
        }

        Vector3 targetPosition = player.position + cameraTargetOffset;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void ExpandView()
    {
        isExpanding = true;
        virtualCamera.Lens.OrthographicSize = Mathf.Lerp(virtualCamera.Lens.OrthographicSize, expandedViewSize, expandSpeed * Time.deltaTime);
    }

    void ResetView()
    {
        isExpanding = false;
        virtualCamera.Lens.OrthographicSize = Mathf.Lerp(virtualCamera.Lens.OrthographicSize, defaultViewSize, expandSpeed * Time.deltaTime);
        cameraTargetOffset = Vector3.Lerp(cameraTargetOffset, Vector3.zero, followSpeed * Time.deltaTime);
    }

}
