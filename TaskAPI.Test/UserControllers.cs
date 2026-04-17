using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TaskAPI.Controllers;
using TaskAPI.Data;
using System.Collections.Generic;
using TaskAPI.Models;
 
namespace TaskAPI.Test
{
    public class UserControllerTests
    {
        [Fact]
        public void GetUsers_ReturnsOkResult_WithUsers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
 
            var context = new AppDbContext(options);
 
            var logger = new Mock<ILogger<UserController>>();
 
            var controller = new UserController(context, logger.Object);
 
            // Act
            var result = controller.GetUsers();
 
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsType<List<User>>(okResult.Value);
        }

        [Fact]
public void GetUsers_ReturnsEmptyList_WhenNoUsers()
{
    var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase("EmptyDb")
        .Options;
 
    var context = new AppDbContext(options);
 
    var logger = new Mock<ILogger<UserController>>();
    var controller = new UserController(context, logger.Object);
 
    var result = controller.GetUsers();
 
    var okResult = Assert.IsType<OkObjectResult>(result);
    var users = Assert.IsType<List<User>>(okResult.Value);
 
    Assert.Empty(users);
}

    [Fact]
public void GetUsers_ReturnsSingleUser()
{
    var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;
 
    var context = new AppDbContext(options);
 
    context.Users.Add(new User { Id = 1, Name = "Suraj", Email = "suraj@gmail.com" });
    context.SaveChanges();
 
    var logger = new Mock<ILogger<UserController>>();
    var controller = new UserController(context, logger.Object);
 
    var result = controller.GetUsers();
 
    var okResult = Assert.IsType<OkObjectResult>(result);
    var users = Assert.IsType<List<User>>(okResult.Value);
 
    Assert.Single(users);
}
    }
}
 