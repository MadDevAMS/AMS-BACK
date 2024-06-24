using AMS.Application.UseCases.User.Command.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Test.Users.Command.CreateUserTest
{

    [TestClass]
    public class CreateUserCommandTest
    {
        private static WebApplicationFactory<Program> _factory = null!;
        private static IServiceScopeFactory _scopeFactory = null!;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        [TestMethod]
        public async Task ShouldNotGenerateToken()
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            //Arange 
            var commmand = new LoginCommand()
            {
                Email = "asdasdaasd@gmail.com",
                Password = "password"
            };

            var expected = 404;

            //Act
            var response = await mediator.Send(commmand);

            //Asser
            Assert.AreEqual(expected, response.Status);
        }
    }
}
