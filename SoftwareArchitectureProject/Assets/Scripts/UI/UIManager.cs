using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public void ToggleActive(GameObject gameObject)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        public void ToggleActive(MonoBehaviour component)
        {
            component.enabled = !component.enabled;
        }
    }
}
