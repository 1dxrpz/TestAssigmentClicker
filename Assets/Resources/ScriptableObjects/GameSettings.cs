using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Project Settings/Game Settings")]
public class GameSettings : ScriptableObject
{
    public string ForecastAPI;
    public string DogAPI;

    public int EnergyPerIteration;
    public int IterationTimeInSeconds;
    public int MaxEnergy;
    public int StartEnergy;
}
