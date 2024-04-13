using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour
{
    public float tickTime = 0.2f;
    public float tickerTimer;
    public delegate void TickAction();
    public static TickAction OnTickAction;

    private void Update()
    {
        tickerTimer += Time.deltaTime;
        if (tickerTimer > tickTime)
        {
            tickerTimer = 0;
            TickEvent();
        }
    }
    private void TickEvent() => OnTickAction?.Invoke();
}
