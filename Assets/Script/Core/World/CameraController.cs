using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public PlayerCTL followTarget;
    private Vector3 targetPos;
    public float moveSpeed;
    private void OnEnable()
    {
        followTarget = FindObjectOfType<PlayerCTL>();
    }
    private void FixedUpdate()
    {
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
