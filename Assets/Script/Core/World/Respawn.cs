using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Respawn : MonoBehaviour
{
    public Transform respawnPos;
    public Vector3 respawnRecord;
    public TextMeshProUGUI respawn_txt;
    public static int expLostRate = 30;
    public static int GoldLostRate = 30;
    private void OnEnable()
    {
        respawn_txt.text = "Respawn In Town (-" + expLostRate + " %Exp And " + GoldLostRate + " %Gold)";
        respawnPos = GameObject.Find("SpawnPoint").GetComponent<Transform>();
    }
    public void RespawnAfterDie()
    {
        Time.timeScale = 1;
        PartyController.player.isAlve = true;
        PartyController.instance.FullRestore();
        PartyController.instance.Respawn(expLostRate, GoldLostRate);
        PartyController.player.transform.position = respawnPos.position;
        PartyController.player.playerhurt.gameOverprefab.SetActive(false);
        PoolManager.instance.DeActivateAllPool();
        LoadSceneManager.instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
