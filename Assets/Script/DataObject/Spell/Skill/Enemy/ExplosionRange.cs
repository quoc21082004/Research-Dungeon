using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class ExplosionRange : MonoBehaviour, ISpell
{
    private ActiveAbility activeAbility;
    private CircleCollider2D mycollider;
    public GameObject explosionRange;
    public Transform alertCircle;
    [Header("Skill Info")]
    public float alertScale;
    public float alertScaleDuration;
    public float lifeTime;
    public TextMeshPro explosionCountDown_txt;
    private void Awake()
    {
        mycollider = GetComponent<CircleCollider2D>();
    }
    private void OnDisable()
    {
        mycollider.enabled = false;
        alertCircle.gameObject.SetActive(false);
        explosionRange.SetActive(false);
    }
    public void KickOff(ActiveAbility ability, Vector2 direction, Quaternion rot)
    {
        activeAbility = ability;
        transform.position = direction;
        alertCircle.localScale = new Vector3(1f, 1f, 1f);
        StartCoroutine(SpellCoroutine());
    }
    private IEnumerator SpellCoroutine()
    {
        alertCircle.gameObject.SetActive(true);
        alertCircle.DOScale(new Vector3(1f, 1f, 1f) * alertScale, alertScaleDuration);
        float tempDuration = alertScaleDuration;
        while (tempDuration > 0)
        {
            explosionCountDown_txt.text = tempDuration.ToString("F1");
            tempDuration -= Time.deltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        mycollider.enabled = true;
        explosionRange.gameObject.SetActive(true);
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHurt>(out var player))
        {
            player.TakeDamage(activeAbility.skillInfo.baseDamage, false);
        }
    }

}