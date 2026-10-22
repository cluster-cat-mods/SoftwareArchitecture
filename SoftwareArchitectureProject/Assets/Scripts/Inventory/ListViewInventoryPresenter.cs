using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

// This class presents items of an inventory in a ListView format.
    public class ListViewInventoryPresenter : InventoryPresenter
    {
        // Indicates whether this inventory belongs to the player
        [SerializeField]
        private bool belongsToPlayer = true;

        // Prefab used to display each item in the inventory list.
        [SerializeField]
        private ItemPresenter itemPresenterPrefab;

        // Parent transform under which item UI elements will be instantiated.
        public Transform listParent;

        // UI text element that displays the name of the current sorting strategy.
        [SerializeField]
        private TextMeshProUGUI sortingStrategyNameText;

        private HashSet<string> uniqueIDs = new();

        private void OnEnable()
        {
            // If the inventory belongs to the player, get it from the player inventory controller singleton.
            if (belongsToPlayer)
                inventory = SingletonInventoryController.Instance.inventory;
            PresentInventory();
        }

        // Populates the inventory list UI with sorted items.
        public override void PresentInventory()
        {
            // Clear any existing item UI elements.
            ClearList();
            uniqueIDs.Clear();

            // Get sorted items from the inventory.
            Item[] items = inventory.GetSortedItems();

            // Instantiate and present each item in the UI.
            for (int i = 0; i < items.Length; i++)
            {
                if (uniqueIDs.Contains(items[i].Id)) continue;

                int count = 0;
                if (items[i].IsStackable)
                {
                    for (int j = 0; j < items.Length; j++)
                    {
                        if (items[j].Id == items[i].Id)  count++;
                    }
                    uniqueIDs.Add(items[i].Id);
                }
                else count = 1;
                ItemPresenter itemPresenter = Instantiate<ItemPresenter>(itemPresenterPrefab);
                itemPresenter.PresentItem(items[i],  count);

                // Set the parent and scale for proper UI layout.
                itemPresenter.transform.SetParent(listParent);
                itemPresenter.transform.localScale = Vector3.one;
            }

            // Update the sorting strategy text if the UI element is assigned.
            if (sortingStrategyNameText != null)
                sortingStrategyNameText.text = inventory.GetCurrentStrategyName();
        }

        // Clears all child item UI elements from the list except the parent itself.
        private void ClearList()
        {
            foreach (Transform transform in listParent.GetComponentsInChildren<Transform>())
            {
                if (transform != listParent)
                    Destroy(transform.gameObject);
            }
        }

        // Handles navigation input for changing the sorting strategy.
        public void Navigate(InputAction.CallbackContext context)
        {
            // Read directional input from the controller/keyboard.
            Vector2 moveVector = context.ReadValue<Vector2>();

            // Navigate to the previous sorting strategy if moving left.
            if (moveVector.x == -1)
            {
                RefreshInventoryWithPrevSorting();
            }

            // Navigate to the next sorting strategy if moving right.
            if (moveVector.x == 1)
            {
                RefreshInventoryWithNextSorting();
            }
        }
    }
