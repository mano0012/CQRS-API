using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Items.GetItem;

public class GetItemHandler : IRequestHandler<GetAllItemsQuery, IEnumerable<GetItemResult>>
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;
    public GetItemHandler(IItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetItemResult>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllItemsAsync();
        return _mapper.Map<IEnumerable<GetItemResult>>(items);
    }
}