using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using CRUD_application_2.Models;
using CRUD_application_2.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_application_2.Tests
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController _controller;
        private List<User> _userList;

        [TestInitialize]
        public void TestInitialize()
        {
            _userList = new List<User>
            {
                new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
                new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" },
                new User { Id = 3, Name = "Test User 3", Email = "test3@example.com" },
            };

            _controller = new UserController();
            UserController.userlist = _userList;
        }

        [TestMethod]
        public void Index_ReturnsCorrectViewWithModel()
        {
            var result = _controller.Index() as ViewResult;
            var model = result?.ViewData.Model as List<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            CollectionAssert.AreEqual(_userList, model);
        }

        [TestMethod]
        public void Details_ReturnsCorrectViewWithModel()
        {
            var result = _controller.Details(1) as ViewResult;
            var model = result?.ViewData.Model as User;

            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.AreEqual(_userList[0], model);
        }

        [TestMethod]
        public void Create_Post_ReturnsRedirectToRouteResult()
        {
            var newUser = new User { Id = 4, Name = "Test User 4", Email = "test4@example.com" };

            var result = _controller.Create(newUser) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            CollectionAssert.Contains(UserController.userlist, newUser);
        }

        [TestMethod]
        public void Edit_Post_ReturnsRedirectToRouteResult()
        {
            var updatedUser = new User { Id = 1, Name = "Updated User", Email = "updated@example.com" };

            var result = _controller.Edit(1, updatedUser) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(updatedUser, UserController.userlist.First(u => u.Id == 1));
        }

        [TestMethod]
        public void Delete_Post_ReturnsRedirectToRouteResult()
        {
            var result = _controller.Delete(1) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsFalse(UserController.userlist.Any(u => u.Id == 1));
        }
    }
}
