using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "GenerationSettings", menuName = "Scriptable Objects/GenerationSettings")]
public class GenerationSettings : ScriptableObject
{
    [Foldout("Dungeon Settings")] public RectInt roomBounds = new RectInt(0, 0, 100, 50);
    [Foldout("Dungeon Settings"), Min(0)] public int minSize = 10;
    [Foldout("Dungeon Settings"), Min(2)] public float divisionRange = 3;
    [Foldout("Dungeon Settings"), Min(0)] public int removeCount = 10;
    [Space] [Foldout("Dungeon Settings")] public int seed;
    [Foldout("Dungeon Settings")] public bool useRandomSeed = true;
    
    public enum Algorithm
    {
        Bfs,
        Dfs,
        DfsRecursive
    }
    [Foldout("Generation Settings")]public Algorithm algorithm = Algorithm.Dfs;

    public enum DelayMode
    {
        Instant, 
        Stepwise, 
        Manual
    } [Foldout("Debug Settings")] public DelayMode delayMode = DelayMode.Instant;
    [Foldout("Debug Settings")] public KeyCode manualKey = KeyCode.Space;
    [Foldout("Debug Settings"), Min(0)] public float stepwiseDelay;
    

}
