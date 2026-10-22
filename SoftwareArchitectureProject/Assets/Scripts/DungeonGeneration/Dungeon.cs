using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace DungeonGeneration
{
    public class Dungeon : MonoBehaviour
    {
        [Expandable] public GenerationSettings settings;
        [HideInInspector] public List<RectInt> roomList = new();
        [HideInInspector] public List<RectInt> doorList = new();
    
        [HideInInspector] public Graph<RectInt> graph  = new();
        [HideInInspector] public TileMap tileMap = new();
    
        [HideInInspector] public bool finishedGenerating = false;
    
   
    }
}

