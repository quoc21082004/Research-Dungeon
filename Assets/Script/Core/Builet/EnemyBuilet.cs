using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilet : MonoBehaviour
{
    private Rigidbody2D myrigid;
    [HideInInspector] public float realdamage;
    private void Awake()
    {
        myrigid = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerCTL>(out var player))
        {
            IDamagable damage = collision.gameObject.GetComponent<IDamagable>();
            if (damage != null)
                damage.TakeDamage(realdamage, false);
            //if (collision.gameObject.TryGetComponent<PlayerCTL>(out var player))
                //player.GetComponent<PlayerHurt>().TakeDamage(realdamage, false);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Wall"))
            gameObject.SetActive(false);
    }

}