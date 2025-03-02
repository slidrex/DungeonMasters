using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cam;
    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
        cam.Follow = _target;
    }
}