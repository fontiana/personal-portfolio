﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using PersonalPortfolio.Controllers;
using PersonalPortfolio.Repository.Project;
using Xunit;

namespace PersonalPortfolio.Tests.Controllers
{
    public class HomeControllerTests
    {
        //private static ServiceProvider Provider;
        //private static ServiceCollection Services;

        [Fact(DisplayName = "Should return a view result with a list of projects")]
        public async Task Projects_ReturnsAViewResult_WithAListOfProjects()
        {
            // Arrange
            var localize = new Mock<IStringLocalizer<HomeController>>();

            var projects = new List<Context.Project>();
            var projectRepository = new Mock<IProjectRepository>();
            projectRepository
                .Setup(repo => repo.GetAsync())
                .ReturnsAsync(projects);

            var controller = new HomeController(localize.Object, projectRepository.Object);

            // Act
            var result = await controller.Portfolio();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
            //    viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());
        }
    }
}