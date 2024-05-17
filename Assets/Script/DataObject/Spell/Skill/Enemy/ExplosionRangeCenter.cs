
using UnityEngine;
public class ExplosionRangeCenter : MonoBehaviour, ISpell
{
    public GameObject explosionprefab;
    public Transform[] explosionPos;

    public void KickOff(ActiveAbility ability, Vector2 direction, Quaternion rot)
    {
        transform.position = PartyController.player.transform.position;
        foreach (var pos in explosionPos)
        {
            if (PoolManager.instance.Release(explosionprefab).TryGetComponent<ISpell>(out var spell))
            {
                spell.KickOff(ability, pos.position, rot);
            }
        }
    }
}