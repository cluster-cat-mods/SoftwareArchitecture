using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public List<RectInt> roomList  = new();
    public List<RectInt> wallList = new ();
    public List<RectInt> doorList = new();
    
    public Graph<RectInt> graph;
    public TileMap tileMap;
    
    private NavMeshSurface _surface;
    

}
