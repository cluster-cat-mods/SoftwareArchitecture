using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDeletor : DungeonGenerator
{
    protected override IEnumerator Generate()
    {
        yield return RemoveRooms();
    }
    
    private IEnumerator RemoveRooms() //O(n)
    {
        nextList = SortList(dungeon.roomList);
        doneList = new List<RectInt>(nextList.Count - dungeon.settings.removeCount);
        for (int i = 0; i < doneList.Capacity; i++)
        {
            currentRoom = nextList[i];
            
            if (dungeon.settings.delayMode != GenerationSettings.DelayMode.Instant) yield return Wait();

            doneList.Add(currentRoom);
        }

        Debug.Log("Removed " + dungeon.settings.removeCount + " rooms");
    }
    
    private List<RectInt> SortList(List<RectInt> listP) //O(n*(n-1)) -> O(n^2)
    {
        bool sorted =  false;
        while (!sorted)
        {
            sorted = true;
            for (int i = 0; i < listP.Count - 1; i++)
            {
                if (listP[i].width * listP[i].height > listP[i + 1].width * listP[i + 1].height)
                {
                    (listP[i], listP[i + 1]) = (listP[i + 1], listP[i]);
                    sorted = false;
                }
            }
        }
        return listP;
    }
}
