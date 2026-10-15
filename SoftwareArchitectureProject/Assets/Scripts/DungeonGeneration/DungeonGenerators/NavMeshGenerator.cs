using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshGenerator : DungeonGenerator
{
    protected override IEnumerator Generate()
    {
        GenerateNavMesh();
        doneList = dungeon.roomList;
        yield return null;
    }

    private void GenerateNavMesh()
    {
        dungeon.GetComponent<NavMeshSurface>().BuildNavMesh();
        Debug.Log("Generated NavMesh");
    }
}
