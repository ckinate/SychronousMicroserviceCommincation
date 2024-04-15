using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Play.Inventory.Service.Entities
{
    public class InventoryItem : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int CatalogItemId { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset AcquiredDate { get; set; }


    }
}