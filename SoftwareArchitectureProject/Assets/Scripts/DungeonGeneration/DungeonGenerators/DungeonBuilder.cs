using System.Collections;
using UnityEngine;

namespace DungeonGeneration.DungeonGenerators
{
    public class DungeonBuilder : DungeonGenerator
    {
        private (int, int)[] _valToPrefab =
        {
            (0, 0),
            (4, 0),
            (4, 0),
            (1, 0),
            (4, 0),
            (4, 0),
            (1, 90),
            (2, 0),
            (4, 0),
            (1, 270),
            (4, 0),
            (2, 270),
            (1, 180),
            (2, 180),
            (2, 90),
            (4, 0)
        };

        private (int, int)[] _doorValsV =
        {
            (1, 180),
            (4, 0),
            (1, 0),
            (0, 0),
            (3, 0),
            (0, 0)
        };
    
        private (int, int)[] _doorValsH =
        {
            (1, 90),
            (0, 0),
            (4, 0),
            (3, 90),
            (1, 270),
            (0, 0)
        };
    
    
        protected override IEnumerator Generate()
        {
            yield return BuildDungeon();
        }
    
        private IEnumerator BuildDungeon()
        {
            GameObject[] children =
            {
                new GameObject("Floor"),
                new GameObject("Walls"),
                new GameObject("Corners"), 
                new GameObject("Doors")
            };
            foreach (var child in children)
            {
                child.transform.parent = dungeon.transform;
            }
        
            Coroutine[] coroutines = new Coroutine[dungeon.roomList.Count];
    
            Debug.Log("starting room coroutines");
            for (int i = 0; i < coroutines.Length; i++)
            {
                coroutines[i] = StartCoroutine(FillRoom(dungeon.roomList[i]));
            }

            Debug.Log("yielding room coroutines");
            foreach (Coroutine coroutine in coroutines)
            {
                yield return coroutine;
            }
        
            coroutines = new Coroutine[dungeon.doorList.Count];
        
            Debug.Log("starting door coroutines");
            for (int i = 0; i < coroutines.Length; i++)
            {
                coroutines[i] = StartCoroutine(FillDoor(dungeon.doorList[i]));
            }
        
            Debug.Log("yielding door coroutines");
            foreach (Coroutine coroutine in coroutines)
            {
                yield return coroutine;
            }
        
            Debug.Log("Dungeon fully build");
        }
    
        private void BuildTile(int row,  int col)
        {
            int val = dungeon.tileMap.GetValue(row, col);
            GameObject go = dungeon.settings.DungeonPrefabs[_valToPrefab[val].Item1];
            if (_valToPrefab[val].Item1 < dungeon.settings.DungeonPrefabs.Length - 1) Instantiate(go, new Vector3(col, 0, row), Quaternion.Euler(new Vector3(0, _valToPrefab[val].Item2, 0)), transform.GetChild(_valToPrefab[val].Item1));
            //scale factor not perfect
        }
    
        private IEnumerator FillRoom(RectInt roomP)
        {
            for (int r = 0; r < roomP.height - 1; r++)
            {
                for (int c = 0; c < roomP.width - 1; c++)
                {
                    BuildTile(roomP.position.y + r, roomP.position.x + c);

                    if (dungeon.settings.DelayMode != GenerationSettings.DelayModeEnum.Instant) yield return Wait();
                }
            }
        
        }

        private IEnumerator FillDoor(RectInt doorP)
        {
            (int, int)[] doorVals = dungeon.tileMap.GetValue(doorP.position.y, doorP.position.x) == 6 ? _doorValsV : _doorValsH;
            int i = 0;
            for (int r = -1; r < doorP.height; r++)
            {
                for (int c = -1; c < doorP.width; c++)
                {
                    if (doorVals[i].Item1 == 4)
                    {
                        i++;
                        continue;
                    }
                    Instantiate(dungeon.settings.DungeonPrefabs[doorVals[i].Item1], new Vector3(doorP.position.x + c, 0, doorP.position.y + r), Quaternion.Euler(new Vector3(0, doorVals[i].Item2)), transform.GetChild(doorVals[i].Item1));
                    i++;
                    if (dungeon.settings.DelayMode != GenerationSettings.DelayModeEnum.Instant) yield return Wait();
                }
            }
        
        }
    
    
    }
}