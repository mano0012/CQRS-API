using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Items.CreateItem;

public class CreateItemHandler : IRequestHandler<CreateItemCommand, CreateItemResult>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateItemHandler
    /// </summary>
    /// <param name="itemRepository">The item repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateItemHandler(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateItemCommand request
    /// </summary>
    /// <param name="command">The CreateItem command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created item details</returns>
    public async Task<CreateItemResult> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateItemCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingItem = await _itemRepository.GetByNameAsync(command.Name, cancellationToken);

        if (existingItem != null)
            throw new DomainException($"Item with name {command.Name} already exists");

        var item = _mapper.Map<Item>(command);
        
        var createdItem = await _itemRepository.CreateAsync(item, cancellationToken);
        var result = _mapper.Map<CreateItemResult>(item);
        return result;
    }
}
