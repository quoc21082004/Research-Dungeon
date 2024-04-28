using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    public void SwitchToggle() => gameObject.SetActive(!gameObject.activeSelf);
}
