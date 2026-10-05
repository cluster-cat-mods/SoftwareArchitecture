using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class SeedGenerator : DungeonGenerator
{
    protected override void StartGenerator(int i)
    {
        if (i == 0) SetSeed();
    }
    
    private void SetSeed() //O(1)
    {
        if (dungeon.settings.useRandomSeed)
        {
            dungeon.settings.seed = DateTime.Now.GetHashCode();
            Debug.Log("Seed: " + dungeon.settings.seed);
        }
        else
        {
            Debug.Log("Seed: " + dungeon.settings.seed + " (Random)");
        }

        Random.InitState(dungeon.settings.seed);

        InvokeOnGeneratorFinished();
    }
    
    
}
