using TMPro;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] ProgressBar healthBar;
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
