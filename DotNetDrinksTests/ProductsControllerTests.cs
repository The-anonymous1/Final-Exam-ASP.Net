using Xunit;
using Microsoft.AspNetCore.Mvc;
using DotNetDrinks.Controllers;
using DotNetDrinks.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DotNetDrinks.Data;

public class ProductsControllerTests
{
    [Fact]
    public async Task Edit_Get_ReturnsViewResult_WithValidId()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "DotNetDrinksDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var product = new Product { Id = 1, Name = "Test Product" };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var controller = new ProductsController(context);

        // Act
        var result = await controller.Edit(1) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Edit", result?.ViewName); // Ensure the view "Edit" is returned
    }

    [Fact]
    public async Task DeleteConfirmed_Post_RemovesProductFromDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "DotNetDrinksDb")
            .Options;
        var context = new ApplicationDbContext(options);

        var product = new Product { Id = 1, Name = "Test Product" };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var controller = new ProductsController(context);

        // Act
        await controller.DeleteConfirmed(1); // Assuming DeleteConfirmed is an async method
        var result = await context.Products.FindAsync(1);

        // Assert
        Assert.Null(result); // Check that the product is no longer in the database
    }
}
