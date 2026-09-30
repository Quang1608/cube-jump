using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level Data/Level")]
public class LevelData : ScriptableObject
{
    public bool isBossFight;
    public Vector3 levelDestination;
}