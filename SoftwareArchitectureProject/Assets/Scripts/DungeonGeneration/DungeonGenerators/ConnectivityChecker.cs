using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectivityChecker : DungeonGenerator
{
    protected override IEnumerator Generate()
    {
        HashSet<RectInt> temp = new();
        bool isConnected = CheckConnectivity(out temp);
        
        Debug.Log(isConnected ? "Graph is fully connected" : "Graph is not fully connected");
        
        doneList = dungeon.roomList;
        yield return null;
    }
}