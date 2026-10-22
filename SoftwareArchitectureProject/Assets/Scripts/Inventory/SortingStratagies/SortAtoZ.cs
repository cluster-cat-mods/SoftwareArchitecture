using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// This is a dummy strategy class, it just convert the item list to an array
/// and return it without any sorting.
/// </summary>
public class SortAtoZ : ItemSortingStrategy
{
    public override Item[] GetSortedItems(List<Item> items)
    {
        return items.OrderBy(i => i.ItemName).ToArray();
    }
}
