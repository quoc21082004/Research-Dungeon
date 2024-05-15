using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TextBar2 : MonoBehaviour
{
    [SerializeField] public Animator myAnim;
    [SerializeField] private Image iconImg;
    [SerializeField] private TextMeshProUGUI value_txt;

    private void Start() => myAnim.GetComponent<Animator>();
    public void SetTextBar(Sprite _iconImg, string _value)
    {
        value_txt.text = _value;
        iconImg.sprite = _iconImg;
        iconImg.SetNativeSize();
    }
    public void Animation_In() => myAnim.Play("NoticeItem_In");
    public void Animation_Out() => myAnim.Play("NoticeItem_Out");
}