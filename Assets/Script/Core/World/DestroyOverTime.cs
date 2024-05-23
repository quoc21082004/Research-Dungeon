using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour    // use for pool object when object can't set (false)
{
    public float timeToDestroy;
    private void Update()
    {
        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
