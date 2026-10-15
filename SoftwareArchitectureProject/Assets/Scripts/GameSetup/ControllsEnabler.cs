using UnityEngine;

public class ControllsEnabler : SetupManager
{
    [SerializeField] private Behaviour[] components;

    protected override void Setup(Dungeon dungeon)
    {
        foreach (Behaviour c in components)
        {
            c.enabled = true;
        }
    }
}
