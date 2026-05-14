using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    [Header("따라갈 대상")]
    [SerializeField] private Transform Target_Player;

    [Header("카메라 이동 속도")]
    [SerializeField] private float _followSpeed = 5f;

    [Header("카메라 거리값")]
    [SerializeField] private Vector3 _offset = new Vector3(8, 3.5f, -10);

    private void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 targetPos = Target_Player.position + _offset;

        transform.position = Vector3.Lerp(transform.position, targetPos, _followSpeed);
    }
}
