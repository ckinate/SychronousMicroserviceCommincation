using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Play.Inventory.Service
{
    public record GrantItemsDto(int UserId, int CatalogItemId, int Quantity);
    public record InventoryItemDto(int CatalogItemId, int Quantity, string Name, string Description, DateTimeOffset AcquireDate);
    public record CatalogItemDto(int Id, string Name, string Description);
}