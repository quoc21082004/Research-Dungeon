using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferPlayer : MonoBehaviour
{
    static PlayerCTL player;
    public string sceneLoad;
    public string transferTo;
    public int sceneID;

    public static string nextTransferSpot;
    public static Vector3 nextDirection;
    private void OnEnable()
    {
        player = GameObject.Find("PlayerCTL").GetComponent<PlayerCTL>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCTL"))
        {
            if (sceneLoad != "") 
            {
                PoolManager.instance.DeActivateAllPool();
                if (LoadSceneManager.instance)
                    LoadSceneManager.instance.LoadScene(sceneID);
            }
            if (transferTo != "")
                nextTransferSpot = transferTo;
        }
    }
    public static void Teleport(Vector3 newcord, Vector2 direction)
    {
        player.gameObject.GetComponent<PlayerCTL>().SetPosition(newcord, direction);
    }
}
