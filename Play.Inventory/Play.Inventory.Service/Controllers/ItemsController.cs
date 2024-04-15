using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Play.Catalog.Service.Repositories;
using Play.Inventory.Service;
using Play.Inventory.Service.Clients;
using Play.Inventory.Service.Entities;
using Play.Inventory.Service.Extension;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem> _itemsRepository;
        private readonly CatalogClients _catalogClients;

        public ItemsController(IRepository<InventoryItem> itemsRepository, CatalogClients catalogClients)
        {
            _itemsRepository = itemsRepository;
            _catalogClients = catalogClients;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(int UserId)
        {
            if (UserId == 0 || UserId.ToString() == null)
            {
                return BadRequest();
            }
            var catalogItems = await _catalogClients.GetCatalogItemsAsync();
            var InventoryItems = await _itemsRepository.GetAllAsync(item => item.UserId == UserId);
            var InventoryItemDtos = InventoryItems.Select(inventoryItem =>
            {
                var catalogItem = catalogItems.Single(item => item.Id == inventoryItem.CatalogItemId);
                return inventoryItem.AsDto(catalogItem.Name, catalogItem.Description);

            });
            return Ok(InventoryItemDtos);
        }



        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantItemsDto grantItemsDto)
        {
            var InventoryItem = await _itemsRepository.GetAsync(item => item.UserId == grantItemsDto.UserId && item.CatalogItemId == item.CatalogItemId);
            if (InventoryItem == null)
            {
                InventoryItem = new InventoryItem
                {
                    CatalogItemId = grantItemsDto.CatalogItemId,
                    UserId = grantItemsDto.UserId,
                    Quantity = grantItemsDto.Quantity,
                    AcquiredDate = DateTimeOffset.Now
                };

                await _itemsRepository.CreateAsync(InventoryItem);
            }
            else
            {
                InventoryItem.Quantity += grantItemsDto.Quantity;
                await _itemsRepository.UpdateAsync(InventoryItem);
            }

            return Ok();
        }






    }
}