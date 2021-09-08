﻿using System;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Ordering.Domain.Entities;
using Microsoft.Extensions.Logging;
using Ordering.Application.Exceptions;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
  public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
  {
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
    {
      _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
      var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);
      if (orderToDelete == null)
      {
        throw new NotFoundException(nameof(Order), request.Id);
      }

      await _orderRepository.DeleteAsync(orderToDelete);
      _logger.LogInformation($"Order {orderToDelete.Id} is successfully deleted.");

      return Unit.Value;
    }
  }
}