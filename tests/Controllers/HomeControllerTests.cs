using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using PersonalPortfolio.Context.Entity;
using PersonalPortfolio.Controllers;
using PersonalPortfolio.Helper;
using PersonalPortfolio.Models;
using PersonalPortfolio.Repository.Post;
using PersonalPortfolio.Repository.Project;
using Xunit;

namespace PersonalPortfolio.Tests.Controllers
{
    public class HomeControllerTests
    {
        public Mock<IStringLocalizer<HomeController>> localize;
        public Mock<IImageHelper> imageHelper;

        public HomeControllerTests()
        {
            localize = new Mock<IStringLocalizer<HomeController>>();
            imageHelper = new Mock<IImageHelper>();
        }


        [Fact(DisplayName = "Should return a view result for index page")]
        public async Task Index_ReturnsAViewResult()
        {
            var posts = new List<PostEntity>();
            posts.Add(new PostEntity { Category = new CategoryEntity(), Title = "Test", ShowcaseImage = "test", PostId = 1, Description = "Test", CreatedAt = DateTime.Now });
            posts.Add(new PostEntity { Category = new CategoryEntity(), Title = "Test", ShowcaseImage = "test" }); 

            var postRepository = new Mock<IPostRepository>();
            postRepository
                .Setup(repo => repo.GetAsync())
                .ReturnsAsync(posts);

            var projects = new List<ProjectEntity>();
            projects.Add(new ProjectEntity { Technologies = new List<TechnologyEntity>(), Title = "Test" });

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository
                .Setup(repo => repo.GetAsync())
                .ReturnsAsync(projects);

            var controller = new HomeController(localize.Object, projectRepository.Object, postRepository.Object, imageHelper.Object);

            // Act
            var result = await controller.IndexAsync();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IndexViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(model);
        }

        [Fact(DisplayName = "Should return a view result with a list of projects")]
        public async Task Portfolio_ReturnsAViewResult_WithAListOfProjects()
        {
            // Arrange
            var localize = new Mock<IStringLocalizer<HomeController>>();

            var projects = new List<ProjectEntity>();
            projects.Add(new ProjectEntity());
            projects.Add(new ProjectEntity());

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository
                .Setup(repo => repo.GetAsync())
                .ReturnsAsync(projects);

            var controller = new HomeController(localize.Object, projectRepository.Object, null, imageHelper.Object);

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

            var project = new ProjectEntity();
            project.Technologies = new List<TechnologyEntity>();

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository
                .Setup(repo => repo.GetByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(project);

            var controller = new HomeController(localize.Object, projectRepository.Object, null, imageHelper.Object);

            // Act
            var result = await controller.Project("Teste");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProjectViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(model);
        }

        [Fact(DisplayName = "Should return a view result for about")]
        public void About_ReturnsAViewResult()
        {
            // Arrange
            var localize = new Mock<IStringLocalizer<HomeController>>();

            var controller = new HomeController(localize.Object, null, null, imageHelper.Object);

            // Act
            var result = controller.About();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }

        [Fact(DisplayName = "Should return a view result for resume")]
        public void Resume_ReturnsAViewResult()
        {
            // Arrange
            var localize = new Mock<IStringLocalizer<HomeController>>();

            var controller = new HomeController(localize.Object, null, null, imageHelper.Object);

            // Act
            var result = controller.Resume();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }

        [Fact(DisplayName = "Should return a view result for blog")]
        public async Task Blog_ReturnsAViewResult()
        {
            // Arrange
            var localize = new Mock<IStringLocalizer<HomeController>>();

            var posts = new List<PostEntity>();
            posts.Add(new PostEntity { Category = new CategoryEntity() });
            posts.Add(new PostEntity { Category = new CategoryEntity() });

            var postRepository = new Mock<IPostRepository>();
            postRepository
                .Setup(repo => repo.GetAsync())
                .ReturnsAsync(posts);

            var controller = new HomeController(localize.Object, null, postRepository.Object, imageHelper.Object);

            // Act
            var result = await controller.Blog();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }

        [Fact(DisplayName = "Should return a view result for category")]
        public async Task Category_ReturnsAViewResult()
        {
            // Arrange
            var localize = new Mock<IStringLocalizer<HomeController>>();

            var posts = new List<PostEntity>();
            posts.Add(new PostEntity { Category = new CategoryEntity() });
            posts.Add(new PostEntity { Category = new CategoryEntity() });

            var postRepository = new Mock<IPostRepository>();
            postRepository
                .Setup(repo => repo.GetByCategoryAsync(It.IsAny<string>()))
                .ReturnsAsync(posts);

            var controller = new HomeController(localize.Object, null, postRepository.Object, imageHelper.Object);

            // Act
            var result = await controller.Category("teste");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }

        [Fact(DisplayName = "Should return a view result for blog post")]
        public async Task Post_ReturnsAViewResult()
        {
            // Arrange
            var localize = new Mock<IStringLocalizer<HomeController>>();

            var post = new PostEntity();

            var postRepository = new Mock<IPostRepository>();
            postRepository
                .Setup(repo => repo.GetByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(post);

            var controller = new HomeController(localize.Object, null, postRepository.Object, imageHelper.Object);

            // Act
            var result = await controller.Post("title-post");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }

        [Fact(DisplayName = "Should return a view result")]
        public void Contact_ReturnsAViewResult()
        {
            // Arrange
            var localize = new Mock<IStringLocalizer<HomeController>>();

            var controller = new HomeController(localize.Object, null, null, imageHelper.Object);

            // Act
            var result = controller.Contact();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }
    }
}
