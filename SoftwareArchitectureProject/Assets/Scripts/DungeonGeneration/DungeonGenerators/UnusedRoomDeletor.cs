using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using UnityEngine;

public class UnusedRoomDeletor : DungeonGenerator
{
    protected override IEnumerator Generate()
    {
        yield return FixConnectivity();
    }
    
    private IEnumerator FixConnectivity() //O(n^2)
    {
        HashSet<RectInt> visited = new();
        bool isConnected = CheckConnectivity(out visited);
        Debug.Log(isConnected ? "Graph already fully connected" : "Graph not yet fully connected");
        
        if (isConnected)
        {
            doneList = dungeon.roomList;
            yield break;
        }

        foreach (RectInt room in dungeon.roomList)
        {
            if (visited.Contains(room))
            {
                doneList.Add(room);
            }
            else
            {
                dungeon.graph.RemoveNode(room);
            }
            
            if (dungeon.settings.delayMode != GenerationSettings.DelayMode.Instant) yield return Wait();
            
        }
        
        int removedDoorsCount = 0;
        for (int i = 0; i < dungeon.doorList.Count; i++)
        {
            if (!visited.Contains(dungeon.doorList[i]))
            {
                dungeon.graph.RemoveNode(dungeon.doorList[i]);
                dungeon.doorList.Remove(dungeon.doorList[i]);
                removedDoorsCount++;
                i--;
            }
            
            if (dungeon.settings.delayMode != GenerationSettings.DelayMode.Instant) yield return Wait();
            
        }
        
        Debug.Log("Removed "  + (dungeon.roomList.Count - doneList.Count) + " unconnected rooms and " + removedDoorsCount + " doors");
    }
}