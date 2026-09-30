using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class DungeonGenerator : MonoBehaviour
{
    protected GenerationSettings settings;
    
    protected Dungeon dungeon;

    protected List<RectInt> nextList;
    protected List<RectInt> doneList;
    protected RectInt currentRoom;
    
    protected IEnumerator Wait() //O(1)
    {
        switch (settings.delayMode)
        {
            case GenerationSettings.DelayMode.Instant:
                break;
            case GenerationSettings.DelayMode.Stepwise:
                yield return new WaitForSeconds(settings.stepwiseDelay);
                break;
            case GenerationSettings.DelayMode.Manual:
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                yield return null;
                break;
        }
    }

    
}
