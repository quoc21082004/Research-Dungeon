using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraZoom : MonoBehaviour
{
    [SerializeField][Range(0, 10f)] private float defaultDistance = 6f;

    [SerializeField][Range(0, 10f)] private float miniumDistance = 1f;

    [SerializeField][Range(0, 10f)] private float maxiumDistnace = 6f;

    [SerializeField][Range(0, 10f)] private float smoothing = 4f;
    [SerializeField][Range(0, 10f)] private float zoomSensitivity = 1f;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineInputProvider inputProvider;
    private float currentTargetDistance;
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        inputProvider = GetComponent<CinemachineInputProvider>();
        currentTargetDistance = defaultDistance;
    }
    private void Zoom()
    {
        float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;
        currentTargetDistance = Mathf.Clamp(currentTargetDistance * zoomValue, miniumDistance, maxiumDistnace);
        float currentDistance = virtualCamera.m_Lens.OrthographicSize;
        if (currentDistance == currentTargetDistance)
            return;

        float lerpZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);
        virtualCamera.m_Lens.OrthographicSize = lerpZoomValue;
    }
}
