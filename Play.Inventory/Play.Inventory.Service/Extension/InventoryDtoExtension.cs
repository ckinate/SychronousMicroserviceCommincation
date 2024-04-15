using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service.Extension
{
    public static class InventoryDtoExtension
    {
        public static InventoryItemDto AsDto(this InventoryItem item, string name, string description)
        {
            return new InventoryItemDto(item.CatalogItemId, item.Quantity, name, description, item.AcquiredDate);
        }

    }
}