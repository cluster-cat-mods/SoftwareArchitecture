using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KeyPressController : MonoBehaviour
{
    [SerializeField] private KeyMap[]  keyMaps;
    
    private void OnEnable()
    {
        foreach (KeyMap keyMap in keyMaps)
        {
            StartCoroutine(keyMap.Coroutine());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    [Serializable]
    private class KeyMap
    {
        [SerializeField] private KeyCode keyCode;
        [SerializeField] private UnityEvent onKeyPressed;

        public IEnumerator Coroutine()
        {
            while (true)
            {
                if (Input.GetKeyDown(keyCode)) onKeyPressed.Invoke();
                yield return null;
            }
        }
    }
}



