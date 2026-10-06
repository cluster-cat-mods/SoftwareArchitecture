using System.Collections;
using UnityEngine;

public class TileMapGenerator : DungeonGenerator
{
    protected override IEnumerator Generate()
    {
        GenerateTileMap();
        Debug.Log("Generated Tilemap");
        
        doneList = dungeon.roomList;
        yield return null;
    }
    
    private void GenerateTileMap()
    {
        dungeon.tileMap = new TileMap(dungeon.settings.roomBounds.height, dungeon.settings.roomBounds.width);
        
        foreach (var room in dungeon.roomList)
        {
            AlgorithmsUtils.FillRectangleOutline(dungeon.tileMap, room, 1);
        }

        foreach (var door in dungeon.doorList)
        {
            AlgorithmsUtils.FillRectangleOutline(dungeon.tileMap, door, 0);
        }
        
    }
    
    
}