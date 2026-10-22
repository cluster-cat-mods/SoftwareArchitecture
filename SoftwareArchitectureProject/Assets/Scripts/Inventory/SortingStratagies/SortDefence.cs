using System.Collections.Generic;
using System.Linq;

namespace Inventory.SortingStratagies
{
    /// <summary>
    /// This is a dummy strategy class, it just convert the item list to an array
    /// and return it without any sorting.
    /// </summary>
    public class SortDefence : ItemSortingStrategy
    {
        public override Item[] GetSortedItems(List<Item> items)
        {
            return  items.OrderByDescending(i => i.Defence).ToArray();
        }
    }
}
