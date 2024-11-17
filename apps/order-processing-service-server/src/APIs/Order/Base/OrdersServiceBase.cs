using Microsoft.EntityFrameworkCore;
using OrderProcessingService.APIs;
using OrderProcessingService.APIs.Common;
using OrderProcessingService.APIs.Dtos;
using OrderProcessingService.APIs.Errors;
using OrderProcessingService.APIs.Extensions;
using OrderProcessingService.Infrastructure;
using OrderProcessingService.Infrastructure.Models;

namespace OrderProcessingService.APIs;

public abstract class OrdersServiceBase : IOrdersService
{
    protected readonly OrderProcessingServiceDbContext _context;

    public OrdersServiceBase(OrderProcessingServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Order
    /// </summary>
    public async Task<Order> CreateOrder(OrderCreateInput createDto)
    {
        var order = new OrderDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Customer = createDto.Customer,
            OrderDate = createDto.OrderDate,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            order.Id = createDto.Id;
        }
        if (createDto.OrderItems != null)
        {
            order.OrderItems = await _context
                .OrderItems.Where(orderItem =>
                    createDto.OrderItems.Select(t => t.Id).Contains(orderItem.Id)
                )
                .ToListAsync();
        }

        if (createDto.Payments != null)
        {
            order.Payments = await _context
                .Payments.Where(payment =>
                    createDto.Payments.Select(t => t.Id).Contains(payment.Id)
                )
                .ToListAsync();
        }

        if (createDto.Shipments != null)
        {
            order.Shipments = await _context
                .Shipments.Where(shipment =>
                    createDto.Shipments.Select(t => t.Id).Contains(shipment.Id)
                )
                .ToListAsync();
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<OrderDbModel>(order.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Order
    /// </summary>
    public async Task DeleteOrder(OrderWhereUniqueInput uniqueId)
    {
        var order = await _context.Orders.FindAsync(uniqueId.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Orders
    /// </summary>
    public async Task<List<Order>> Orders(OrderFindManyArgs findManyArgs)
    {
        var orders = await _context
            .Orders.Include(x => x.OrderItems)
            .Include(x => x.Shipments)
            .Include(x => x.Payments)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return orders.ConvertAll(order => order.ToDto());
    }

    /// <summary>
    /// Meta data about Order records
    /// </summary>
    public async Task<MetadataDto> OrdersMeta(OrderFindManyArgs findManyArgs)
    {
        var count = await _context.Orders.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Order
    /// </summary>
    public async Task<Order> Order(OrderWhereUniqueInput uniqueId)
    {
        var orders = await this.Orders(
            new OrderFindManyArgs { Where = new OrderWhereInput { Id = uniqueId.Id } }
        );
        var order = orders.FirstOrDefault();
        if (order == null)
        {
            throw new NotFoundException();
        }

        return order;
    }

    /// <summary>
    /// Update one Order
    /// </summary>
    public async Task UpdateOrder(OrderWhereUniqueInput uniqueId, OrderUpdateInput updateDto)
    {
        var order = updateDto.ToModel(uniqueId);

        if (updateDto.OrderItems != null)
        {
            order.OrderItems = await _context
                .OrderItems.Where(orderItem =>
                    updateDto.OrderItems.Select(t => t).Contains(orderItem.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Payments != null)
        {
            order.Payments = await _context
                .Payments.Where(payment => updateDto.Payments.Select(t => t).Contains(payment.Id))
                .ToListAsync();
        }

        if (updateDto.Shipments != null)
        {
            order.Shipments = await _context
                .Shipments.Where(shipment =>
                    updateDto.Shipments.Select(t => t).Contains(shipment.Id)
                )
                .ToListAsync();
        }

        _context.Entry(order).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Orders.Any(e => e.Id == order.Id))
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
    /// Connect multiple OrderItems records to Order
    /// </summary>
    public async Task ConnectOrderItems(
        OrderWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Orders.Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .OrderItems.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.OrderItems);

        foreach (var child in childrenToConnect)
        {
            parent.OrderItems.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple OrderItems records from Order
    /// </summary>
    public async Task DisconnectOrderItems(
        OrderWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Orders.Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .OrderItems.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.OrderItems?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple OrderItems records for Order
    /// </summary>
    public async Task<List<OrderItem>> FindOrderItems(
        OrderWhereUniqueInput uniqueId,
        OrderItemFindManyArgs orderFindManyArgs
    )
    {
        var orderItems = await _context
            .OrderItems.Where(m => m.OrderId == uniqueId.Id)
            .ApplyWhere(orderFindManyArgs.Where)
            .ApplySkip(orderFindManyArgs.Skip)
            .ApplyTake(orderFindManyArgs.Take)
            .ApplyOrderBy(orderFindManyArgs.SortBy)
            .ToListAsync();

        return orderItems.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple OrderItems records for Order
    /// </summary>
    public async Task UpdateOrderItems(
        OrderWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] childrenIds
    )
    {
        var order = await _context
            .Orders.Include(t => t.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .OrderItems.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        order.OrderItems = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Payments records to Order
    /// </summary>
    public async Task ConnectPayments(
        OrderWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Orders.Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Payments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Payments);

        foreach (var child in childrenToConnect)
        {
            parent.Payments.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Payments records from Order
    /// </summary>
    public async Task DisconnectPayments(
        OrderWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Orders.Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Payments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Payments?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Payments records for Order
    /// </summary>
    public async Task<List<Payment>> FindPayments(
        OrderWhereUniqueInput uniqueId,
        PaymentFindManyArgs orderFindManyArgs
    )
    {
        var payments = await _context
            .Payments.Where(m => m.OrderId == uniqueId.Id)
            .ApplyWhere(orderFindManyArgs.Where)
            .ApplySkip(orderFindManyArgs.Skip)
            .ApplyTake(orderFindManyArgs.Take)
            .ApplyOrderBy(orderFindManyArgs.SortBy)
            .ToListAsync();

        return payments.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Payments records for Order
    /// </summary>
    public async Task UpdatePayments(
        OrderWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] childrenIds
    )
    {
        var order = await _context
            .Orders.Include(t => t.Payments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Payments.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        order.Payments = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Shipments records to Order
    /// </summary>
    public async Task ConnectShipments(
        OrderWhereUniqueInput uniqueId,
        ShipmentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Orders.Include(x => x.Shipments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Shipments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Shipments);

        foreach (var child in childrenToConnect)
        {
            parent.Shipments.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Shipments records from Order
    /// </summary>
    public async Task DisconnectShipments(
        OrderWhereUniqueInput uniqueId,
        ShipmentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Orders.Include(x => x.Shipments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Shipments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Shipments?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Shipments records for Order
    /// </summary>
    public async Task<List<Shipment>> FindShipments(
        OrderWhereUniqueInput uniqueId,
        ShipmentFindManyArgs orderFindManyArgs
    )
    {
        var shipments = await _context
            .Shipments.Where(m => m.OrderId == uniqueId.Id)
            .ApplyWhere(orderFindManyArgs.Where)
            .ApplySkip(orderFindManyArgs.Skip)
            .ApplyTake(orderFindManyArgs.Take)
            .ApplyOrderBy(orderFindManyArgs.SortBy)
            .ToListAsync();

        return shipments.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Shipments records for Order
    /// </summary>
    public async Task UpdateShipments(
        OrderWhereUniqueInput uniqueId,
        ShipmentWhereUniqueInput[] childrenIds
    )
    {
        var order = await _context
            .Orders.Include(t => t.Shipments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Shipments.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        order.Shipments = children;
        await _context.SaveChangesAsync();
    }
}
