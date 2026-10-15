using System;
using UnityEngine;

public abstract class SetupManager : MonoBehaviour
{
    private void OnEnable()
    {
        DungeonGenerator.OnDungeonGenerationFinished += Setup;
    }

    private void OnDisable()
    {
        DungeonGenerator.OnDungeonGenerationFinished -= Setup;
    }

    protected abstract void Setup(Dungeon dungeon);

}
