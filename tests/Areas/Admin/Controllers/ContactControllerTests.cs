﻿using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Areas.Admin.Controllers;
using Xunit;

namespace PersonalPortfolio.Tests.Areas.Admin.Controllers
{
    public class ContactControllerTests
    {
        [Fact(DisplayName = "Should return a view result for index page")]
        public void Index_ReturnsAViewResult()
        {
            var controller = new ContactController();

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }
    }
}
