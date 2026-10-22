using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

namespace DungeonGeneration.DungeonGenerators
{
    public class NavMeshGenerator : DungeonGenerator
    {
        protected override IEnumerator Generate()
        {
            GenerateNavMesh();
            yield return null;
        }

        private void GenerateNavMesh()
        {
            dungeon.GetComponent<NavMeshSurface>().BuildNavMesh();
            Debug.Log("Generated NavMesh");
        }
    }
}
