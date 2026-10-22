using System.Collections;
using UnityEngine;
using UnityEngine.LightTransport.PostProcessing;

namespace DungeonGeneration.DungeonGenerators
{
    public class ObjectSpawner : DungeonGenerator
    {
        protected override IEnumerator Generate()
        {
            Coroutine[] coroutines = new Coroutine[dungeon.roomList.Count];
    
            Debug.Log("starting room coroutines");
            for (int i = 0; i < coroutines.Length; i++)
            {
                coroutines[i] = StartCoroutine(SpawnObjects(i));
            }
            
            Debug.Log("yielding room coroutines");
            foreach (Coroutine coroutine in coroutines)
            {
                yield return coroutine;
            }

        }

        private IEnumerator SpawnObjects(int index)
        {
            GameObject[] prefabs = dungeon.settings.RoomObjectPatterns[Random.Range(0, dungeon.settings.RoomObjectPatterns.Length)].Prefabs;
            RectInt room = dungeon.roomList[index];
            float dist;
            int num = 0;
            Vector3 pos = new Vector3(room.xMin, 0.05f, room.yMin);
            float width = room.width - 2;
            float height = room.height - 2;
            
            if (width > height + height / 4)
            {
                dist = height / 4;
                pos += new  Vector3(dist, 0, dist);
                Vector3 pos2 = new Vector3(room.xMin, 0.05f, room.yMin) + new Vector3(height + (width - height) / 2, 0, dist);
                
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Instantiate(prefabs[num], pos + new Vector3(dist * j, 0, dist * i), Quaternion.identity);
                        num++;
                        
                        if (dungeon.settings.DelayMode != GenerationSettings.DelayModeEnum.Instant) yield return Wait();
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    Instantiate(prefabs[num], pos2 + new Vector3(0, 0, dist * i), Quaternion.identity);
                    num++;
                        
                    if (dungeon.settings.DelayMode != GenerationSettings.DelayModeEnum.Instant) yield return Wait();
                }
                
            }
            else if (room.width + room.width / 4 < room.height)
            {
                dist = width / 4;
                pos += new  Vector3(dist, 0, dist);
                Vector3 pos2 = new Vector3(room.xMin, 0.05f, room.yMin) + new Vector3(dist, 0, width + (height - width) / 2);
                
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Instantiate(prefabs[num], pos + new Vector3(dist * i, 0, dist * j), Quaternion.identity);
                        num++;
                        
                        if (dungeon.settings.DelayMode != GenerationSettings.DelayModeEnum.Instant) yield return Wait();
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    Instantiate(prefabs[num], pos2 + new Vector3(dist * i, 0, 0), Quaternion.identity);
                    num++;
                        
                    if (dungeon.settings.DelayMode != GenerationSettings.DelayModeEnum.Instant) yield return Wait();
                }
            }
            else
            {
                dist = width / 4;
                pos += new  Vector3(dist, 0, dist);
                
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Instantiate(prefabs[num], pos + new Vector3(dist * j, 0, dist * i), Quaternion.identity);
                        num++;
                        
                        if (dungeon.settings.DelayMode != GenerationSettings.DelayModeEnum.Instant) yield return Wait();
                    }
                }
            }
            
        }
    }
    
    
}
