using Microsoft.EntityFrameworkCore;
using OrderProcessingService.APIs;
using OrderProcessingService.APIs.Common;
using OrderProcessingService.APIs.Dtos;
using OrderProcessingService.APIs.Errors;
using OrderProcessingService.APIs.Extensions;
using OrderProcessingService.Infrastructure;
using OrderProcessingService.Infrastructure.Models;

namespace OrderProcessingService.APIs;

public abstract class OrderItemsServiceBase : IOrderItemsService
{
    protected readonly OrderProcessingServiceDbContext _context;

    public OrderItemsServiceBase(OrderProcessingServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one OrderItem
    /// </summary>
    public async Task<OrderItem> CreateOrderItem(OrderItemCreateInput createDto)
    {
        var orderItem = new OrderItemDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Product = createDto.Product,
            Quantity = createDto.Quantity,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            orderItem.Id = createDto.Id;
        }
        if (createDto.Order != null)
        {
            orderItem.Order = await _context
                .Orders.Where(order => createDto.Order.Id == order.Id)
                .FirstOrDefaultAsync();
        }

        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<OrderItemDbModel>(orderItem.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one OrderItem
    /// </summary>
    public async Task DeleteOrderItem(OrderItemWhereUniqueInput uniqueId)
    {
        var orderItem = await _context.OrderItems.FindAsync(uniqueId.Id);
        if (orderItem == null)
        {
            throw new NotFoundException();
        }

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many OrderItems
    /// </summary>
    public async Task<List<OrderItem>> OrderItems(OrderItemFindManyArgs findManyArgs)
    {
        var orderItems = await _context
            .OrderItems.Include(x => x.Order)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return orderItems.ConvertAll(orderItem => orderItem.ToDto());
    }

    /// <summary>
    /// Meta data about OrderItem records
    /// </summary>
    public async Task<MetadataDto> OrderItemsMeta(OrderItemFindManyArgs findManyArgs)
    {
        var count = await _context.OrderItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one OrderItem
    /// </summary>
    public async Task<OrderItem> OrderItem(OrderItemWhereUniqueInput uniqueId)
    {
        var orderItems = await this.OrderItems(
            new OrderItemFindManyArgs { Where = new OrderItemWhereInput { Id = uniqueId.Id } }
        );
        var orderItem = orderItems.FirstOrDefault();
        if (orderItem == null)
        {
            throw new NotFoundException();
        }

        return orderItem;
    }

    /// <summary>
    /// Update one OrderItem
    /// </summary>
    public async Task UpdateOrderItem(
        OrderItemWhereUniqueInput uniqueId,
        OrderItemUpdateInput updateDto
    )
    {
        var orderItem = updateDto.ToModel(uniqueId);

        if (updateDto.Order != null)
        {
            orderItem.Order = await _context
                .Orders.Where(order => updateDto.Order == order.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(orderItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.OrderItems.Any(e => e.Id == orderItem.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Order record for OrderItem
    /// </summary>
    public async Task<Order> GetOrder(OrderItemWhereUniqueInput uniqueId)
    {
        var orderItem = await _context
            .OrderItems.Where(orderItem => orderItem.Id == uniqueId.Id)
            .Include(orderItem => orderItem.Order)
            .FirstOrDefaultAsync();
        if (orderItem == null)
        {
            throw new NotFoundException();
        }
        return orderItem.Order.ToDto();
    }
}
