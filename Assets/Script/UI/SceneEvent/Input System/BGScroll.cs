using UnityEngine;
using UnityEngine.UI;

public class BGScroll : MonoBehaviour
{
    [SerializeField] private RawImage rawImg;
    [SerializeField] private float point_x, point_y;
    private void Update() => rawImg.uvRect = new Rect(rawImg.uvRect.position + new Vector2(point_x, point_y) * Time.deltaTime, rawImg.uvRect.size);
}
