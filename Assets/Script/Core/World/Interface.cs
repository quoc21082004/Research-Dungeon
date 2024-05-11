using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public interface IItem      
{
    void Destroy();
    string GetDescription();
}
public interface IActiveAbility
{
    float CastDelay { get; }
    float MaxUseRange { get; }
    bool IsEnoughMana();
    Respond TryUse();

}
public interface ISpell 
{
    void KickOff(ActiveAbility ability, Vector2 direction , Quaternion rot);
}
public interface IHotKey
{
    void UpdateCoolDown();
    bool IsHotKeyCoolDown(int numkey);
    void UseHotKey(int numkey);
}
public interface IInteract
{
    void Interact();
}
public interface ISkill
{
    void UseSkill1();
    void UseSkill2();
    void UseSkill3();
    void UseSkill4();
}
public interface IGUI
{
     void GetReference(GameManager _gameManager);
     void UpdateDataGUI();
}