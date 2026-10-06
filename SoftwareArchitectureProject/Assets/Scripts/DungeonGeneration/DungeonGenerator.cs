using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Dungeon))]
public abstract class DungeonGenerator : MonoBehaviour
{
    [SerializeField] protected bool isGenerationStartPoint;
    [SerializeField] protected DungeonGenerator nextGenerator;
    
    protected Dungeon dungeon;
    protected List<RectInt> nextList = new();
    protected List<RectInt> doneList = new();
    protected RectInt currentRoom = new();
    protected RectInt otherCurrentRoom = new();
    protected RectInt[] lastArray = new RectInt[2];
    

#if  UNITY_EDITOR
    [SerializeField] protected bool debugDraw = true;
    [SerializeField, ShowIf("debugDraw")] protected Color nextColor = Color.darkGray;
    [SerializeField, ShowIf("debugDraw")] protected Color selectedColor = Color.cyan;
    [SerializeField, ShowIf("debugDraw")] protected Color otherSelectedColor = Color.magenta;
    [SerializeField, ShowIf("debugDraw")] protected Color doneColor = Color.green;
#endif

    private void OnEnable()
    {
        dungeon = GetComponent<Dungeon>();
    }

    private void Start()
    {
        if (isGenerationStartPoint)
        {
            ClearDungeon();
            dungeon.roomList.Add(dungeon.settings.roomBounds);
            StartCoroutine(RunGenerator());
            Debug.Log("Started dungeon generation " + this);
        }
    }

    private IEnumerator RunGenerator()
    {
        yield return Generate();
        dungeon.roomList = new List<RectInt>(doneList);
        ClearGenerator();
        
        if (nextGenerator != null)
        {
            Debug.Log("Started next generator: " + nextGenerator);
            StartCoroutine(nextGenerator.RunGenerator());
        }
        else
        {
            Debug.Log("Finished generating dungeon");
        }
    }
    
    protected abstract IEnumerator Generate();


    private void ClearGenerator()
    {
        nextList.Clear();
        doneList.Clear();
        currentRoom = new();
        otherCurrentRoom = new();
        lastArray = new RectInt[2];
    }

    private void ClearDungeon()
    {
        dungeon.roomList = new();
        dungeon.wallList = new();
        dungeon.doorList = new();
    }
    
    
    
    protected IEnumerator Wait() //O(1)
    {
        switch (dungeon.settings.delayMode)
        {
            case GenerationSettings.DelayMode.Instant:
                break;
            case GenerationSettings.DelayMode.Stepwise:
                yield return new WaitForSeconds(dungeon.settings.stepwiseDelay);
                break;
            case GenerationSettings.DelayMode.Manual:
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                yield return null;
                break;
        }
    }
    
    


#if UNITY_EDITOR
    private void Update()
    {
        if (debugDraw)
        {
            foreach (var room in nextList) AlgorithmsUtils.DebugRectInt(room, nextColor);
            foreach (var room in doneList) AlgorithmsUtils.DebugRectInt(room, doneColor);
            AlgorithmsUtils.DebugRectInt(currentRoom, selectedColor);
            AlgorithmsUtils.DebugRectInt(otherCurrentRoom, otherSelectedColor);
            foreach (var room in lastArray) AlgorithmsUtils.DebugRectInt(room, otherSelectedColor);
        }
        
    }
#endif
    
}
