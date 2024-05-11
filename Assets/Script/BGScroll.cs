using UnityEngine;
using UnityEngine.UI;

public class BGScroll : MonoBehaviour
{
    [SerializeField] private RawImage rawImg;
    [SerializeField] private float point_x, point_y;
    public GameObject a, b, c, d, e;
    private void Update()
    {
        rawImg.uvRect = new Rect(rawImg.uvRect.position + new Vector2(point_x, point_y) * Time.deltaTime, rawImg.uvRect.size);
        a.transform.position = Vector3.MoveTowards(a.transform.position, b.transform.position, 2 * Time.deltaTime);
        c.transform.position = Vector3.Lerp(c.transform.position, d.transform.position, 2 * Time.deltaTime);
        e.transform.position = new Vector3(5f, 5f, e.transform.position.z);

    }
}
