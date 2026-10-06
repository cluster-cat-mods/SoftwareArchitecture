using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.AI.Navigation;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public GenerationSettings settings;
    [HideInInspector] public List<RectInt> roomList = new();
    [HideInInspector] public List<RectInt> doorList = new();
    
    [HideInInspector] public Graph<RectInt> graph  = new();
    [HideInInspector] public TileMap tileMap = new();
    
    private NavMeshSurface _surface;
}

