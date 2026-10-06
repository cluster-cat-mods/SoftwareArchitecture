using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGenerator : DungeonGenerator
{
    protected override IEnumerator Generate()
    {
        yield return GenerateRooms();
    }

    private IEnumerator GenerateRooms()
    {
        while (dungeon.roomList.Count != 0)
        {
            foreach (RectInt room in dungeon.roomList)
            {
                currentRoom = room;

                if (dungeon.settings.delayMode != GenerationSettings.DelayMode.Instant) yield return Wait();
                
                if (room.width / 2 < dungeon.settings.minSize && room.height / 2 < dungeon.settings.minSize)
                {
                    doneList.Add(room);
                }
                else
                {
                    lastArray = SplitRoom(room);

                    if (dungeon.settings.delayMode != GenerationSettings.DelayMode.Instant) yield return Wait();

                    nextList.Add(lastArray[0]);
                    nextList.Add(lastArray[1]);
                }

                if (dungeon.settings.delayMode != GenerationSettings.DelayMode.Instant) yield return Wait();
            }
            dungeon.roomList = nextList;
            nextList = new();
        }
        Debug.Log("Generated " + doneList.Count + " rooms");
        
    }
    
    private RectInt[] SplitRoom(RectInt roomToSplitP) //O(1)
    {
        bool splitVertical;
        if (roomToSplitP.width / 2 < dungeon.settings.minSize || roomToSplitP.height / 2 < dungeon.settings.minSize)
        {
            splitVertical = !(roomToSplitP.width / 2 < dungeon.settings.minSize);
        }
        else
        {
            splitVertical = Random.Range(0, 2) == 1;
        }
        
        RectInt[] newRooms = new RectInt[2]
        {
            new (roomToSplitP.position, roomToSplitP.size),
            new (roomToSplitP.position, roomToSplitP.size)
        };

        float divider = Random.Range(2, dungeon.settings.divisionRange);

        if (splitVertical)
        {
            newRooms[0].width = Mathf.RoundToInt(newRooms[0].width / divider);
            newRooms[0].width = Mathf.Max(newRooms[0].width, dungeon.settings.minSize);
            newRooms[0].width = Mathf.Min(newRooms[0].width, roomToSplitP.width - dungeon.settings.minSize);
            newRooms[1].width -= newRooms[0].width;
            newRooms[1].x += newRooms[0].width;
        }
        else
        {
            newRooms[0].height = Mathf.RoundToInt(newRooms[0].height / divider);
            newRooms[0].height = Mathf.Max(newRooms[0].height, dungeon.settings.minSize);
            newRooms[0].height = Mathf.Min(newRooms[0].height, roomToSplitP.height - dungeon.settings.minSize);
            newRooms[1].height -= newRooms[0].height;
            newRooms[1].y += newRooms[0].height;
        }
        return newRooms;
    }
}
