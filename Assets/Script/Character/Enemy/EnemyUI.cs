using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] ProgressBar healthBar;
    [SerializeField] Image maxhp_img;
    [SerializeField] Image hp_img;
    [SerializeField] TextMeshProUGUI monsterLv_txt;

    #region Main Method
    private void OnEnable()
    {
        enemy = GetComponentInParent<Enemy>();
        monsterLv_txt.text = $"(Lv.{enemy.level}";

        healthBar.OnInitValue(enemy.Health.currentValue, enemy.Health.maxValue);
        enemy.Health.OnValueChangeEvent += healthBar.OnCurrentValueChange;
    }
    private void OnDisable() => enemy.Health.OnValueChangeEvent -= healthBar.OnCurrentValueChange;
    #endregion
}
