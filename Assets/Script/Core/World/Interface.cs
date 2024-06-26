using UnityEngine;
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
public interface IGUI
{
     void GetReference(GameManager _gameManager);
     void UpdateDataGUI();
}