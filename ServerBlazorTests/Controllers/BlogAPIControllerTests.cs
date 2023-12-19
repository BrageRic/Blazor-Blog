using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerBlazor.Server.Controllers;
using ServerBlazor.Models;
using ServerBlazor.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServerBlazor.Data;
using ServerBlazor.Hubs;

namespace ServerBlazor.Server.Controllers.Tests
{
    [TestClass()]
    public class BlogAPIControllerTests
    {
        private Mock<IBlogRepository> _repository;
        [TestMethod()]
        public async Task GetPost_ReturnsPost()
        {
            // Arrange
            var _repository = new Mock<IBlogRepository>();
            var hubContextMock = new Mock<IHubContext<NotiHub, INotiClient>>();

            var postId = 4;
            var expectedPosts = GetTestPosts();
            _repository.Setup(repo => repo.GetPost(postId)).Returns(expectedPosts[3]);

            // Create an instance of the controller with the mocked dependencies
            var controller = new BlogAPIController(_repository.Object, hubContextMock.Object);

            // Act
            var result = controller.GetPost(postId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actionResult = (OkObjectResult)result;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Post));
            var model = (Post)actionResult.Value;
            Assert.AreEqual(expectedPosts[0], model);
        }

        private List<Post> GetTestPosts()
        {
            var testProducts = new List<Post>
            {
                new Post { PostId = 1, Title = "test1", Content = "Ctest1", BlogId = 1 },
                new Post { PostId = 2, Title = "test2", Content = "Ctest2", BlogId = 1 },
                new Post { PostId = 3, Title = "test3", Content = "Ctest3", BlogId = 1 },
                new Post { PostId = 4, Title = "test4", Content = "Ctest4", BlogId = 1 }
            };

            return testProducts;
        }
    }
}