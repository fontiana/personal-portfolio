using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using PersonalPortfolio.Controllers;
using PersonalPortfolio.Models;
using PersonalPortfolio.Repository.Project;
using Xunit;

namespace PersonalPortfolio.Tests.Controllers
{
    public class HomeControllerTests
    {
        //private static ServiceProvider Provider;
        //private static ServiceCollection Services;

        [Fact(DisplayName = "Should return a view result")]
        public void Index_ReturnsAViewResult()
        {
            // Arrange
            var localize = new Mock<IStringLocalizer<HomeController>>();

            var controller = new HomeController(localize.Object, null, null);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }

        [Fact(DisplayName = "Should return a view result with a list of projects")]
        public async Task Portfolio_ReturnsAViewResult_WithAListOfProjects()
        {
            // Arrange
            var localize = new Mock<IStringLocalizer<HomeController>>();

            var projects = new List<Context.Project>();
            projects.Add(new Context.Project());
            projects.Add(new Context.Project());

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository
                .Setup(repo => repo.GetAsync())
                .ReturnsAsync(projects);

            var controller = new HomeController(localize.Object, projectRepository.Object, null);

            // Act
            var result = await controller.Portfolio();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<ProjectViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact(DisplayName = "Should return a view result with the detailed project")]
        public async Task Project_ReturnsAViewResult()
        {
            // Arrange
            var localize = new Mock<IStringLocalizer<HomeController>>();

            var projects = new List<Context.Project>();

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository
                .Setup(repo => repo.GetAsync())
                .ReturnsAsync(projects);

            var controller = new HomeController(localize.Object, projectRepository.Object, null);

            // Act
            var result = await controller.Project(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProjectViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(model);
        }
    }
}
