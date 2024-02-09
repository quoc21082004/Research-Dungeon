using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandFireCenter : MonoBehaviour, ISpell
{
    [SerializeField] GameObject handfireprefab;
    [SerializeField] int count;
    private ActiveAbility activeAbility;
    public void KickOff(ActiveAbility ability, Vector2 dir, Quaternion rot)
    {
        activeAbility = ability;
        Transform handpos = GameObject.Find("HandFirePosition").GetComponent<Transform>();
        if (handpos != null)
            transform.position = handpos.transform.position;
        for (int i = 0; i < count; i++)
        {
            var direction = Random.insideUnitCircle;
            if (PoolManager.instance.Release(handfireprefab).TryGetComponent<BossStraightBuilet>(out var spell))
            {
                spell.KickOff(ability, direction, Quaternion.identity);
                spell.transform.parent = transform;
                spell.transform.SetParent(transform);
            }
        }
    }
}
