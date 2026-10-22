using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonGeneration
{
    [CreateAssetMenu(fileName = "GenerationSettings", menuName = "Scriptable Objects/GenerationSettings")]
    public class GenerationSettings : ScriptableObject
    {
        [SerializeField, Foldout("Dungeon Settings")] private RectInt roomBounds = new RectInt(0, 0, 100, 50);
        [SerializeField, Foldout("Dungeon Settings"), Min(0)] private int minSize = 10;
        [SerializeField, Foldout("Dungeon Settings"), Min(2)] private float divisionRange = 3;
        [SerializeField, Foldout("Dungeon Settings"), Range(0, 1)] private float fractionToRemove = 0.2f;
    
        [SerializeField, Foldout("Generation Settings")] private bool useRandomSeed = true;
        [Foldout("Generation Settings"), DisableIf("UseRandomSeed")] public int seed;
        public enum AlgorithmEnum
        {
            Bfs,
            Dfs,
            DfsRecursive
        }
        [SerializeField, Foldout("Generation Settings")] private AlgorithmEnum algorithm = AlgorithmEnum.Dfs;

        [SerializeField, Foldout("Building Settings")] private GameObject[] dungeonPrefabs = new GameObject[5];

        public enum DelayModeEnum
        {
            Instant, 
            Stepwise, 
            Manual
        } [SerializeField, Foldout("Debug Settings")] private DelayModeEnum delayMode = DelayModeEnum.Instant;
        [SerializeField, Foldout("Debug Settings"), ShowIf("DelayMode", DelayModeEnum.Manual)] private KeyCode manualKey = KeyCode.Space;
        [SerializeField, Foldout("Debug Settings"), ShowIf("DelayMode", DelayModeEnum.Stepwise), Min(0)] private float stepwiseDelay;
        
        
        public RectInt RoomBounds => roomBounds;
        public int MinSize => minSize;
        public float DivisionRange => divisionRange;
        public float FractionToRemove => fractionToRemove;
        
        public bool UseRandomSeed => useRandomSeed;
        public AlgorithmEnum Algorithm => algorithm;
        
        public GameObject[] DungeonPrefabs => dungeonPrefabs;
        
        public DelayModeEnum DelayMode => delayMode;
        public KeyCode ManualKey => manualKey;
        public float  StepwiseDelay => stepwiseDelay;
        
    }
    
    
}
