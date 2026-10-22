using UnityEngine;

public class ObjectEnabler : SetupManager
{
    [SerializeField] private GameObject[] objects;

    protected override void Setup(Dungeon dungeon)
    {
        foreach (GameObject go in objects)
        {
            go.SetActive(true);
        }
        Debug.Log("Enabled " + objects.Length + " objects");
    }
}
