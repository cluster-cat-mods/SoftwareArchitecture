using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public void ToggleActive(GameObject gameObject)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
