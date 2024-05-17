using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ExplosionMelee : MonoBehaviour, ISpell
{
    private ActiveAbility activeAbility;
    public GameObject explosionprefab;
    public Transform alertCircle;
    private CircleCollider2D mycollider;
    [Header("Info Skill")]
    public float explosionRadius;
    public float alertScale;
    public float alertScaleDuration;
    public float lifeTime;
    public TextMeshPro explosionCountDown_txt;
    private Coroutine spellCoroutine;
    private void Awake() => mycollider = GetComponent<CircleCollider2D>();
    private void OnDisable()
    {
        alertCircle.gameObject.SetActive(false);
        explosionprefab.gameObject.SetActive(false);
        mycollider.enabled = false;
    }
    public void KickOff(ActiveAbility ability, Vector2 position, Quaternion rot)
    {
        activeAbility = ability;
        transform.position = position;
        alertCircle.localScale = new Vector3(1f, 1f, 1f);
        //StartCoroutine(SpellCoroutine(activeAbility));
        if (spellCoroutine != null)
            StopCoroutine(spellCoroutine);
        StartCoroutine(SpellCoroutine(ability));
    }
    private IEnumerator SpellCoroutine(ActiveAbility ability)
    {
        gameObject.SetActive(true);
        mycollider.enabled = false;
        alertCircle.gameObject.SetActive(true);
        alertCircle.transform.position = transform.position;
        alertCircle.DOScale(new Vector3(1f, 1f, 1f) * alertScale, alertScaleDuration);
        float tempAlertDuration = alertScaleDuration;
        while (tempAlertDuration > 0) 
        {
            explosionCountDown_txt.text = tempAlertDuration.ToString("F1");
            tempAlertDuration -= Time.deltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        explosionprefab.gameObject.SetActive(true);
        mycollider.enabled = true;
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHurt>(out var player))
        {
            IDamagable damage = collision.GetComponent<IDamagable>();
            if (damage != null)
                damage.TakeDamage(activeAbility.skillInfo.baseDamage, false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}