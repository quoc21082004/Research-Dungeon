using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damagable : MonoBehaviour 
{
    public abstract void TakeDamage(float amount, bool isCrit);
    public abstract float CaculateDMG(float amt);
}