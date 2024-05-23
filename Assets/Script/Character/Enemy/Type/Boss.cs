using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyRange
{
    public List<GameObject> monsterprefab;
    public Transform[] prefabPos;

    protected override void Direction()
    {
        throw new System.NotImplementedException();
    }
    protected override IEnumerator attackDelay()
    {
        throw new System.NotImplementedException();
    }
}
