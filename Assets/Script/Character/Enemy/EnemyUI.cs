using TMPro;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private TextMeshProUGUI monsterLv_txt;

    #region Main Method
    private void OnEnable()
    {
        monsterLv_txt.text = $"(Lv.{enemy.level}";

        healthBar.OnInitValue(enemy.Health.currentValue, enemy.Health.maxValue);
        enemy.Health.OnValueChangeEvent += healthBar.OnCurrentValueChange;
    }
    private void OnDisable() => enemy.Health.OnValueChangeEvent -= healthBar.OnCurrentValueChange;
    #endregion
}
