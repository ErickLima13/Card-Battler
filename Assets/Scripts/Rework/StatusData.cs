using UnityEngine;

[CreateAssetMenu(fileName = "StatusData", menuName = "Scriptable Objects/StatusData")]
public class StatusData : ScriptableObject
{
    public string displayName;
    public Sprite icon;
    public Color color;
    public string description;
}