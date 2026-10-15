using System;
using UnityEngine;
using NaughtyAttributes;

public class PlayerSpawner : SetupManager
{
    [SerializeField] private GameObject player;

    protected override void Setup(Dungeon dungeon)
    {
        if (player == null) throw new NullReferenceException("Player prefab is null");
        SpawnPlayer(dungeon.graph);
        Debug.Log("Spawned player");
    }

    private void SpawnPlayer(Graph<RectInt> graph) //O(1)
    {
        Instantiate(player, new Vector3(graph.GetNodes()[1].center.x, transform.position.y + 1, graph.GetNodes()[1].center.y), Quaternion.identity);
    }
}

