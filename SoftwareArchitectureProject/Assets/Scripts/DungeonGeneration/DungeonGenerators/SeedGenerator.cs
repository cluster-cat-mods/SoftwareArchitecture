using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class SeedGenerator : DungeonGenerator
{
    protected override IEnumerator Generate()
    {
        SetSeed();
        doneList = dungeon.roomList;
        yield return null;
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
    }
    
    
}
