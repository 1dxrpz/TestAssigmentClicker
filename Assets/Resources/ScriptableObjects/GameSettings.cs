using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Project Settings/Game Settings")]
public class GameSettings : ScriptableObject
{
    public string ForecastAPI;
    public string DogAPI;

    public int MaxEnergy;
    public int StartEnergy;

    public int EnergyAutogainAmount = 10;
    public int EnergyAutogainDuration = 10;

    public int AutoclickDuration = 3;

    public int ClickEnergyCost = 1;
    public int CurrencyPerClick = 1;
}
