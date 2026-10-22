using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ToggleActive(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
