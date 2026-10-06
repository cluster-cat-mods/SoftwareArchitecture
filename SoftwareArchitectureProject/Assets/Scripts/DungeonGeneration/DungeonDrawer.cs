using System;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Dungeon))]
public class DungeonDrawer : MonoBehaviour
{
    [SerializeField] private bool drawBounds = true;
    [SerializeField, ShowIf("drawBounds")] private Color boundsColor = Color.yellow;
    [SerializeField] private bool drawRooms = true;
    [SerializeField, ShowIf("drawRooms")] private Color roomColor = Color.yellow;
    [SerializeField] private bool drawDoors = true;
    [SerializeField, ShowIf("drawDoors")] private Color doorColor = Color.cyan;
    [SerializeField] private bool drawGraph = true;
    [SerializeField, ShowIf("drawGraph")] private Color graphColor = Color.white;
    
    private Dungeon _dungeon;
    private DungeonGenerator[] _generators;
    private RectInt _minSizeBounds;

    private void Start()
    {
        _dungeon = GetComponent<Dungeon>();
        _generators = GetComponents<DungeonGenerator>();
    }

    private void Update()
    {
        DrawDebug();
    }
    
    
    private void DrawDebug()
    {
        if (_dungeon.roomList.Count < 2 && drawBounds)
        {
            AlgorithmsUtils.DebugRectInt(_dungeon.settings.roomBounds, boundsColor);

            _minSizeBounds = new RectInt(_dungeon.settings.roomBounds.position, new Vector2Int(_dungeon.settings.minSize, _dungeon.settings.minSize));
            AlgorithmsUtils.DebugRectInt(_minSizeBounds, boundsColor);
        }
        
        if (drawRooms) 
        {
            foreach (var room in _dungeon.roomList) AlgorithmsUtils.DebugRectInt(room, roomColor);
            
        }
        if (drawDoors) foreach (var door in _dungeon.doorList)
        {
            AlgorithmsUtils.DebugRectInt(door, doorColor);
        }
        if (drawGraph)
        {
            foreach (var node in _dungeon.graph.GetNodes())
            {
                foreach (var edge in _dungeon.graph.GetNeighbors(node))
                {
                    Debug.DrawLine(new Vector3(node.center.x, 0, node.center.y), new Vector3(edge.center.x, 0, edge.center.y), graphColor);
                }
            }
        }
        
        
    }
    
    
}
