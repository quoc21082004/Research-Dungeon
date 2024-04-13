using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpellCenter : MonoBehaviour, ISpell
{
    public int spikeCount;
    public GameObject spikeprefab;
    private ActiveAbility activeAbility;
    public void KickOff(ActiveAbility ability, Vector2 direction, Quaternion rot)
    {
        activeAbility = ability;
        Vector3 startRoomBoss = new Vector3(-10.97f - transform.position.x, 9.17f - transform.position.y, 0f);
        Vector3 endRoomBoss = new Vector3(18.41f - transform.position.x, -20.92f - transform.position.y, 0f);
        for (int i = 0; i < spikeCount; i++) 
        {
            Vector3 spikePos = new Vector3(Random.Range(startRoomBoss.x, endRoomBoss.x), Random.Range(startRoomBoss.y, endRoomBoss.y), 0f);
            if (i == 0)
                spikePos = PartyController.player.transform.position;
            if (PoolManager.instance.Release(spikeprefab).TryGetComponent<SpikeSpell>(out var spell))
            {
                spell.KickOff(ability, spikePos, Quaternion.identity);
            }
        }
    }
}
