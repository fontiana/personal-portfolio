using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Controllers;
using PersonalPortfolio.Models;
using Xunit;

namespace PersonalPortfolio.Tests.Controllers
{
    public class ErrorControllerTests
    {
        [Fact(DisplayName = "Should return a view result with an error model")]
        public void Projects_ReturnsAViewResult_WithAnErrorModel()
        {
            // Arrange
            var controller = new ErrorController();
            var errorModel = new ErrorViewModel {
                RequestId = "Error"
            };

            // Act
            var result = controller.Index(errorModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal("Error", model.RequestId);
        }

        [Fact(DisplayName = "Should return a view result with an error model not found")]
        public void Projects_ReturnsAViewResult_WithAnErrorModelNotFound()
        {
            // Arrange
            var controller = new ErrorController();
            var errorModel = new ErrorViewModel
            {
                RequestId = "NotFound"
            };

            // Act
            var result = controller.NotFound(errorModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal("NotFound", model.RequestId);
        }
    }
}
