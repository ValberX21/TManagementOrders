using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TManagementOrders.API.Controllers;
using TManagementOrders.Application.Service;
using TManagementOrders.Domain.Entities;

namespace TManagementOrders.Tests
{
    public class Client_Test
    {
        private readonly ClientController _controller;
        private readonly Mock<ClientService> _mockClientService;

        public Client_Test()
        {
            _mockClientService = new Mock<ClientService>();
            _controller = new ClientController(_mockClientService.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewWithClients()
        {
            // Arrange
            _mockClientService
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<Client> { new Client { Name = "Valber" } });

            // Act
            var result = await _controller.Index(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ClientFilterViewModel>(viewResult.Model);
            Assert.Single(model.Clients);
        }
    }
}
