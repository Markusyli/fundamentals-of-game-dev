using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnLevelWasLoaded(int level)
    {
        var target = PlayerManager.instance.player.transform.Find("FollowTarget");
        freeLookCamera.LookAt = target.transform;
        freeLookCamera.Follow = target.transform;
    }

    public Camera mainCamera;
    public CinemachineFreeLook freeLookCamera;
}
