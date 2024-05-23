using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    public void SwitchToggle() => gameObject.SetActive(!gameObject.activeSelf);
}
