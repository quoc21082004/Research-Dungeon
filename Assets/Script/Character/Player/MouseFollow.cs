using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    PlayerCTL player;
    private void OnEnable()
    {
        player = GetComponentInParent<PlayerCTL>();
    }
    private void Update()
    {
        if (player.isActiveAndEnabled)
            FaceMouse();
    }
    public void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
        if (PlayerCTL.isFace)
            transform.localScale = new Vector3(1, -1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}
