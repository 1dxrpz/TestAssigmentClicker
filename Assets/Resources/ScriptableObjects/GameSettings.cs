using System;
using UnityEngine;

[Serializable]
public class ApiEndpoints
{
    public string Base;
    public string DogBreeds;
    public string DogBreedId;
    public string RandomFact;
}

[CreateAssetMenu(fileName = "GameSettings", menuName = "Project Settings/Game Settings")]
public class GameSettings : ScriptableObject
{
    public string ForecastAPI;
    public ApiEndpoints DogAPI = new();

    public int MaxEnergy;
    public int StartEnergy;

    public int EnergyAutogainAmount = 10;
    public int EnergyAutogainDuration = 10;

    public int AutoclickDuration = 3;

    public int ClickEnergyCost = 1;
    public int CurrencyPerClick = 1;
}
