using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.AI.Navigation;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public GenerationSettings settings;
    public List<RectInt> roomList = new();
    public List<RectInt> wallList = new ();
    public List<RectInt> doorList = new();
    
    public Graph<RectInt> graph;
    public TileMap tileMap;
    
    private NavMeshSurface _surface;

    public event Action<int> OnNextGenerator;
    private bool _generatorFinished = false;

    private void NextGenerator()
    {
        _generatorFinished = true;
    }
    
    private IEnumerator GenerateDungeon()
    {
        for (int i = 0; i < settings.EnabledGenerators.Length; i++)
        {
            if (settings.EnabledGenerators[i]) OnNextGenerator?.Invoke(i);
            yield return new WaitUntil(() => _generatorFinished);
            _generatorFinished = false;
        }
    }

    private void OnEnable()
    {
        DungeonGenerator.OnGeneratorFinshed += NextGenerator;
    }

    private void OnDisable()
    {
        DungeonGenerator.OnGeneratorFinshed -= NextGenerator;
    }

    [Button]
    public void StartGenerator()
    {
        roomList.Clear();
        roomList.Add(new RectInt(settings.roomBounds.position, settings.roomBounds.size));
        
        StartCoroutine(GenerateDungeon());
    }
}
