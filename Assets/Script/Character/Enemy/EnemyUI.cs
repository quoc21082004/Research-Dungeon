using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] Image maxhp_img;
    [SerializeField] Image hp_img;
    [SerializeField] TextMeshProUGUI monsterLv_txt;
    private void OnEnable()
    {
        enemy = GetComponentInParent<Enemy>();
        if (enemy.enemystat.Type != TypeEnemy.Boss)
            monsterLv_txt.color = new Color(0f, 255f, 218f, 255f);
        else
            monsterLv_txt.color = new Color(255f, 130f, 121f, 255f);
        monsterLv_txt.text =  "(Lv" + enemy.level.ToString() + ")";
    }
    private void Update()
    {
        hp_img.fillAmount = enemy.health / enemy.maxhealth;
    }
}
