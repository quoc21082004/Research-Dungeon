using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactivate : MonoBehaviour
{
    [SerializeField] private bool destroyGameObject;
    [SerializeField] private float lifeTime;
    private void OnEnable()
    {
        StartCoroutine(DeactivateCourtine());
    }
    private IEnumerator DeactivateCourtine()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
