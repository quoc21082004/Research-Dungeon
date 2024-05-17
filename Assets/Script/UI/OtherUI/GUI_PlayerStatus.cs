using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI_PlayerStatus : MonoBehaviour, IGUI
{
    [SerializeField] private PlayerCTL player;
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private ProgressBar manaBar;
    [SerializeField] private TextMeshProUGUI level_txt;
    [SerializeField] private Slider expProgress;

    #region Main Method
    private void Start()
    {
        if (player == null)
            player = PartyController.player.GetComponent<PlayerCTL>();
        expProgress.minValue = 0;

    }
    private void OnEnable() => RegisterEvent();
    private void OnDisable() => UnRegisterEvent();
    #endregion

    #region Resurb Method
    private void RegisterEvent()
    {
        if (!player) return;
        var _gameManager = GameManager.instance;
        var _currentLevel = _gameManager.level;

        level_txt.text = $"Lv.{_currentLevel}";
        expProgress.value = _gameManager.exp;
        expProgress.maxValue = _gameManager.upgradeSO.GetNextLevel(_currentLevel);

        player.Health.OnInitValueEvent += healthBar.OnInitValue;
        player.Health.OnMaxValueChangeEvent += healthBar.OnMaxValueChange;
        player.Health.OnValueChangeEvent += healthBar.OnCurrentValueChange;

        player.Mana.OnInitValueEvent += manaBar.OnInitValue;
        player.Mana.OnMaxValueChangeEvent += manaBar.OnMaxValueChange;
        player.Mana.OnValueChangeEvent += manaBar.OnCurrentValueChange;
    }
    private void UnRegisterEvent()
    {
        if (!player) return;
        player.Health.OnInitValueEvent -= healthBar.OnInitValue;
        player.Health.OnMaxValueChangeEvent -= healthBar.OnMaxValueChange;
        player.Health.OnValueChangeEvent -= healthBar.OnCurrentValueChange;

        player.Mana.OnInitValueEvent -= manaBar.OnInitValue;
        player.Mana.OnMaxValueChangeEvent -= manaBar.OnMaxValueChange;
        player.Mana.OnValueChangeEvent -= manaBar.OnCurrentValueChange;
    }
    #endregion

    public void OpenHUD() => gameObject.SetActive(true);
    public void CloseHUD() => gameObject.SetActive(false);
   
    #region Interface Method
    public void GetReference(GameManager _gameManager)
    {
    }
    public void UpdateDataGUI() { }
    #endregion

}