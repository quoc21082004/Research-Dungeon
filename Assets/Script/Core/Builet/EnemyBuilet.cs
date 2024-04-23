using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private Rigidbody2D myrigid;
    [HideInInspector] public float realdamage;
    private void Awake()
    {
        myrigid = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCTL") || collision.gameObject.CompareTag("Wall")) 
        {
            if (collision.gameObject.TryGetComponent<PlayerCTL>(out PlayerCTL player))
                player.GetComponent<PlayerHurt>().TakeDamage(realdamage, false);
            gameObject.SetActive(false);
        }
    }

}
