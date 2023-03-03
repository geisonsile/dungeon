using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public string displayName;
    public GameObject objectPrefab;
}
