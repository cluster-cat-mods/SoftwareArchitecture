using System.Collections;
using UnityEngine;
public class GraphGenerator : DungeonGenerator
{
    protected override IEnumerator Generate()
    {
        yield return GenerateDoorsAndGraph();
    }
    
    private IEnumerator GenerateDoorsAndGraph()
    {
        for (int i = 0; i < dungeon.roomList.Count; i++)
        {
            currentRoom = dungeon.roomList[i];

            RectInt biggerRoom = dungeon.roomList[i];
            biggerRoom.size += new Vector2Int(4, 4);
            biggerRoom.position += new Vector2Int(-2, -2);
            
            for (int j = 0; j < dungeon.roomList.Count; j++)
            {
                if (j == i) continue;
                
                otherCurrentRoom = dungeon.roomList[j];

                RectInt overlap = AlgorithmsUtils.Intersect(biggerRoom, otherCurrentRoom);

                if (overlap is { width: <= 2, height: <= 2 }) continue;

                if (overlap.position.x != currentRoom.position.x)
                {
                    overlap.x += overlap.position.x > currentRoom.position.x ? -1 : 1;
                }       
                if (overlap.position.y != currentRoom.position.y)
                {
                    overlap.y += overlap.position.y > currentRoom.position.y ? -1 : 1;
                }        
                
                RectInt door = GenerateDoor(overlap);
                if (door != new RectInt())
                {
                    dungeon.doorList.Add(door);
                    dungeon.graph.AddEdge(door, currentRoom);
                    dungeon.graph.AddEdge(door, otherCurrentRoom);
                }
                        
                if (dungeon.settings.delayMode != GenerationSettings.DelayMode.Instant) yield return Wait();
                
            }

            if (dungeon.graph.GetNodes().Contains(currentRoom))
            {
                doneList.Add(currentRoom);
                
            }
            dungeon.roomList.Remove(currentRoom);
            i--;

        }

        Debug.Log("Generated " + dungeon.doorList.Count + " doors and created a graph with: " + dungeon.graph.GetNodeCount() + " nodes");
    }
    
    private RectInt GenerateDoor(RectInt overlapP) //O(1)
    {
        if (overlapP.width * overlapP.height <= 12) return new();
        
        RectInt door = overlapP;
        if (overlapP.width > overlapP.height)
        {
            door.width = overlapP.height;
            door.x += Random.Range(door.width * 3, overlapP.width - door.width * 3) - door.width / 2;
        }
        else
        {
            door.height = overlapP.width;
            door.y += Random.Range(door.height * 3, overlapP.height - door.height * 3) - door.height / 2;
        }
        return door;

    }
}