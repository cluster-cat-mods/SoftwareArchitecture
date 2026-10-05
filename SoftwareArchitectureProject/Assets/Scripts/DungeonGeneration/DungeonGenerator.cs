using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DungeonGenerator : MonoBehaviour
{
    protected Dungeon dungeon;

    protected List<RectInt> nextList = new();
    protected List<RectInt> doneList = new();
    protected RectInt currentRoom = new();
    protected RectInt otherCurrentRoom = new();
    protected RectInt[] lastArray = new RectInt[2];

    public static event Action OnGeneratorFinshed;

#if  UNITY_EDITOR
    [SerializeField] protected Color nextColor = Color.darkGray;
    [SerializeField] protected Color selectedColor = Color.cyan;
    [SerializeField] protected Color otherSelectedColor = Color.magenta;
    [SerializeField] protected Color doneColor = Color.green;
#endif

    protected abstract void StartGenerator(int i);

    protected void InvokeOnGeneratorFinished()
    {
        OnGeneratorFinshed?.Invoke();
    }

    protected void Clear()
    {
        nextList.Clear();
        doneList.Clear();
        currentRoom = new RectInt();
        otherCurrentRoom = new RectInt();
        lastArray = new RectInt[2];
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
    
    private void OnEnable()
    {
        dungeon = GetComponent<Dungeon>();
        dungeon.OnNextGenerator += StartGenerator;
    }

    private void OnDisable()
    {
        dungeon.OnNextGenerator -= StartGenerator;
    }
    

#if UNITY_EDITOR
    private void Update()
    {
        foreach (var room in nextList) AlgorithmsUtils.DebugRectInt(room, nextColor);
        foreach (var room in doneList) AlgorithmsUtils.DebugRectInt(room, doneColor);
        AlgorithmsUtils.DebugRectInt(currentRoom, selectedColor);
        AlgorithmsUtils.DebugRectInt(otherCurrentRoom, otherSelectedColor);
        foreach (var room in lastArray) AlgorithmsUtils.DebugRectInt(room, otherSelectedColor);
    }
#endif
    
}
