using UnityEngine;

/// <summary>
/// This class presents inventory items to GUI, it can change item's sorting
/// order by using different sorting strategies of the inventory class.
/// </summary>
public abstract class InventoryPresenter : MonoBehaviour
{
    [SerializeField]
    protected InventoryModel inventory;

    public abstract void PresentInventory();
    public void RefreshInventoryWithPrevSorting()
    {
        inventory.PreviousSortingStrategy();
        PresentInventory();
    }
    public void RefreshInventoryWithNextSorting()
    {
        inventory.NextSortingStrategy();
        PresentInventory();
    }
}