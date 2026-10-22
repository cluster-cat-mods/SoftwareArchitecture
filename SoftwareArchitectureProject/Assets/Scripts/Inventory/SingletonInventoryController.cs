using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// A singleton player inventory controller, it ensures that player's inventory
    /// is shared in different scenes.
    /// </summary>
    [RequireComponent(typeof(InventoryModel))]
    public class SingletonInventoryController : MonoBehaviour
    {
        public InventoryModel inventory { get; private set; }
        public static SingletonInventoryController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            if(inventory == null)
            {
                inventory = GetComponent<InventoryModel>();
            }
            if(inventory != null)
            {
                ItemContainerModel.onGetItem += inventory.AddItem;
            }
        }

        private void OnDisable()
        {
            if (inventory != null)
            {
                ItemContainerModel.onGetItem -= inventory.AddItem;
            }
        }
    }
}