using DungeonGeneration;
using UnityEngine;
using UnityEngine.Events;

namespace GameSetup
{
    public class EventRunner : SetupManager
    {
        [SerializeField] private UnityEvent onGenerationFinished;

        protected override void Setup(Dungeon dungeon)
        {
            onGenerationFinished.Invoke();
            Debug.Log("Invoked onGenerationFinished");
        }
    }
}
