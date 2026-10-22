using UnityEngine;

namespace DungeonGeneration
{
    [CreateAssetMenu(fileName = "RoomObjectPattern", menuName = "Scriptable Objects/RoomObjectPattern")]
    public class RoomObjectPattern : ScriptableObject
    {
        [SerializeField] private GameObject[] prefabs = new GameObject[9];
        public GameObject[] Prefabs => prefabs;
    }
}