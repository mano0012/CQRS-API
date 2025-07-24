using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleByCustomer;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByCustomer;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing Sales operations
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SalesController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Customer))]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);

        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException("Invalid User Id claim");
        }

        command.UpdateCustomerId(userId);

        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<CreateSaleResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a sale by ID
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleByIdRequest { SaleId = id };
        var validator = new GetSaleByIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var query = _mapper.Map<GetSaleByIdQuery>(request);

        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException("Invalid User Id claim");
        }

        query.UpdateCustomerId(userId);

        var response = await _mediator.Send(query, cancellationToken);

        return Ok(_mapper.Map<GetSaleByIdResponse>(response));
    }

    /// <summary>
    /// Retrieves all sales by Authenticated Customer
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale list of the current Customer</returns>
    [HttpGet()]
    [ProducesResponseType(typeof(ApiResponseWithData<IEnumerator<GetSaleByCustomerResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetSaleByCustomer(CancellationToken cancellationToken)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException("Invalid User Id claim");
        }

        var request = new GetSaleByCustomerRequest {
            CustomerId = userId
        };

        var validator = new GetSaleByCustomerRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var query = _mapper.Map<GetSaleByCustomerQuery>(request);

        var response = await _mediator.Send(query, cancellationToken);

        return Ok(_mapper.Map<List<GetSaleByCustomerResponse>>(response));
    }

    [HttpGet("Customer/{id}")]
    [Authorize(Roles = $"{nameof(UserRole.Manager)},{nameof(UserRole.Admin)}")]
    [ProducesResponseType(typeof(ApiResponseWithData<IEnumerator<GetSaleByCustomerResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetSaleByCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleByCustomerRequest
        {
            CustomerId = id
        };

        var validator = new GetSaleByCustomerRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var query = _mapper.Map<GetSaleByCustomerQuery>(request);

        var response = await _mediator.Send(query, cancellationToken);

        return Ok(_mapper.Map<List<GetSaleByCustomerResponse>>(response));
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = $"{nameof(UserRole.Manager)},{nameof(UserRole.Admin)}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        request.SaleId = id;

        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateSaleCommand>(request);

        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<UpdateSaleResponse>(response));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{nameof(UserRole.Manager)},{nameof(UserRole.Admin)}")]
    [ProducesResponseType(typeof(ApiResponseWithData<DeleteSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CancelSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleRequest
        {
            SaleId = id
        };

        var validator = new DeleteSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleCommand>(request);

        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<DeleteSaleResponse>(response));
    }

    [HttpDelete("{saleId}/Item/{itemId}")]
    [Authorize(Roles = $"{nameof(UserRole.Manager)},{nameof(UserRole.Admin)}")]
    [ProducesResponseType(typeof(ApiResponseWithData<DeleteSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CancelSaleItem([FromRoute] Guid saleId, [FromRoute] Guid itemId, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleItemRequest
        {
            SaleId = saleId,
            ItemId = itemId
        };

        var validator = new DeleteSaleItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleItemCommand>(request);

        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<DeleteSaleItemResponse>(response));
    }
}
