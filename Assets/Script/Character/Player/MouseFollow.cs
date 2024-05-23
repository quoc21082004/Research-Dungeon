using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private PlayerCTL player;
    private void OnEnable() => player = GetComponentInParent<PlayerCTL>();
    public void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
        if (player.mySR.flipX) 
            transform.localScale = new Vector3(1, -1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}
