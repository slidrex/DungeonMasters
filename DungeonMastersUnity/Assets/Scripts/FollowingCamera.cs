using Unity.Cinemachine;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    private CinemachineCamera _camera;
    private Transform _target;

    private void Start()
    {
        _camera = GetComponent<CinemachineCamera>();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _camera.Follow = _target;
    }
}