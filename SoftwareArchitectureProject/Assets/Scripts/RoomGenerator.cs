using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGenerator : DungeonGenerator
{
    private enum DelayMode
    {
        Instant, 
        Stepwise, 
        Manual
    } [SerializeField] private DelayMode delayMode = DelayMode.Instant;
    [SerializeField] private KeyCode manualKey = KeyCode.Space;
    [SerializeField, Min(0)] private float stepwiseDelay;
    
    private void SetSeed() //O(1)
    {
        if (settings.useRandomSeed)
        {
            settings.seed = DateTime.Now.GetHashCode();
            Debug.Log("Seed: " + settings.seed);
        }
        else
        {
            Debug.Log("Seed: " + settings.seed + " (Random)");
        }

        Random.InitState(settings.seed);
    }
    
    private IEnumerator GenerateRooms()
    {
        while (dungeon.roomList.Count != 0)
        {
            foreach (RectInt room in dungeon.roomList)
            {
                currentRoom = room;

                if (delayMode !=DelayMode.Instant)
                {
                    yield return Wait();
                }
                
                if (room.width / 2 < minSize && room.height / 2 < minSize)
                {
                    _doneList.Add(room);
                }
                else
                {
                    _lastArray = SplitRoom(room);

                    if (generationMode != GenerationMode.instantanious)
                    {
                        yield return Wait();
                    }

                    _nextList.Add(_lastArray[0]);
                    _nextList.Add(_lastArray[1]);
                }

                if (generationMode != GenerationMode.instantanious)
                {
                    yield return Wait();
                }
            }
            _roomList = _nextList;
            _nextList = new();
        }
        _lastArray = new RectInt[2];
        _currentRoom = new();
        
        Debug.Log("Generated " + _doneList.Count + " rooms");
    }
}
