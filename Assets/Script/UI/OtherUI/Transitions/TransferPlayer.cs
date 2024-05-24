using UnityEngine;

public class TransferPlayer : MonoBehaviour
{
    public static PlayerCTL player;
    public string sceneLoad;
    public string transferTo;
    public int sceneID;

    public static string nextTransferSpot;
    public static Vector3 nextDirection;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
        PartyController.player.GetComponent<PlayerCTL>().SetPosition(newcord, direction);
    }
}
