﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : Singleton<PartyController>
{
    public static event Action<int> onCoinChanged;
    public static Player player;
    public static Inventory inventoryG;
    private void Update()
    {
        if (player == null)
            player = transform.GetChild(0).gameObject.GetComponent<Player>();

        if (inventoryG == null)
            inventoryG = new Inventory { Gold = player.playerdata.otherStats.gold };
        player.playerdata.otherStats.gold = inventoryG.Gold;

        /*if (ItemHotKeyManager.instance == null)
            return;
        else
        {
            bool[] hotkeyInputs = new bool[2]
            {
                Input.GetKeyDown(KeyCode.X),
                Input.GetKeyDown(KeyCode.C),
            };
            for (int i = 0; i < ItemHotKeyManager.instance.NumOfHotKeyItem; i++)
                if (hotkeyInputs[i] && !ItemHotKeyManager.instance.IsHotKeyCoolDown(i)) // cool down = false (run)
                    ItemHotKeyManager.instance.UseHotKey(i);
        }

        if (SpellHotKeyManager.instance == null)
            return;
        else
        {
            bool[] hotkeySpell = new bool[4]
            {
                Input.GetKeyDown(KeyCode.Alpha1),
                Input.GetKeyDown(KeyCode.Alpha2),
                Input.GetKeyDown(KeyCode.Alpha3),
                Input.GetKeyDown(KeyCode.Alpha4),
            };
            for (int i = 0; i < SpellHotKeyManager.instance.NumOfHotKeySpell; i++)
                if (hotkeySpell[i] && !SpellHotKeyManager.instance.IsHotKeyCoolDown(i))
                    SpellHotKeyManager.instance.UseHotKey(i);
        }
        */
    }
    public void Respawn(int lostRateExp,int lostRateGold)
    {
        player.isAlve = true;
        player.gameObject.SetActive(true);
        GameManager.instance.RespawnAfterDie(lostRateExp);
        inventoryG.Gold -= (int)((lostRateGold * PartyController.inventoryG.Gold) / 100);
    }
    public void FullRestore()
    {
        player.health = Mathf.Max(player.health, player.maxhealth);
        player.mana += Mathf.Max(player.mana, player.maxmana);
    }
    public static void IncreaseCoin(int amount)
    {
        inventoryG.Gold = Mathf.Clamp(inventoryG.Gold + amount, 0, Int32.MaxValue);
        SendCoinEvent();
    }
    public static void SendCoinEvent() => onCoinChanged?.Invoke(inventoryG.Gold);
    public static void AddExperience(float amount) => GameManager.instance.AddExperience(amount);
}
