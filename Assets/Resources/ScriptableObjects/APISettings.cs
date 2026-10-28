using UnityEngine;

[CreateAssetMenu(fileName = "APISettings", menuName = "Project Settings/APISettings")]
public class APISettings : ScriptableObject
{
    public string ForecastAPI;
    public string DogAPI;
}
