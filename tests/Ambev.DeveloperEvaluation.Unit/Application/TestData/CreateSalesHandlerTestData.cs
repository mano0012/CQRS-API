using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSalesHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated users will have valid:
    /// - Username (using internet usernames)
    /// - Password (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Customer or Admin)
    /// </summary>
    private static readonly Faker<CreateSaleItemCommand> createSaleItemCommandFaker = new Faker<CreateSaleItemCommand>()
        .RuleFor(u => u.ItemId, f => Guid.NewGuid())
        .RuleFor(u => u.Quantity, f => 10);

    private static readonly Faker<CreateSaleCommand> createSaleCommandFaker = new Faker<CreateSaleCommand>()
        .RuleFor(u => u.BranchName, f => "Teste")
        .RuleFor(u => u.CustomerId, f => Guid.NewGuid())
        .RuleFor(u => u.Items, f => new List<CreateSaleItemCommand> { createSaleItemCommandFaker.Generate() });

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static CreateSaleCommand GenerateSaleValidCommand()
    {
        return createSaleCommandFaker.Generate();
    }
    public static CreateSaleCommand GenerateItemValidCommand()
    {
        return createSaleCommandFaker.Generate();
    }
}
