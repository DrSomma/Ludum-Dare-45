using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("UI/slider")]
    public Slider hpSlider;
    public Slider hungerSlider;
    public Slider thirstSlider;
    public Slider energySlider;
    public Slider hygieneSlider;
    public TextMeshProUGUI txtMoney;

    private PlayerManager playerManager;
    private PlayerUpgrades playerUpgrades;

    // Start is called before the first frame update
    void Start()
    {
        playerUpgrades = PlayerUpgrades.instance;
        playerManager = PlayerManager.Instance;
        playerManager.onStatsChangeCallback += UpdateUI;
    }

    void UpdateUI()
    {
        hpSlider.value = (playerManager.hp / playerUpgrades.maxHP);
        hungerSlider.value = (playerManager.hunger / playerUpgrades.maxHunger);
        thirstSlider.value = (playerManager.thirst / playerUpgrades.maxThirst);
        energySlider.value = (playerManager.energy / playerUpgrades.maxEnergy); 
        hygieneSlider.value = (playerManager.hygiene / playerUpgrades.maxHygiene);
        txtMoney.text = playerManager.money + "$";
        //TODO: Money
    }
}
