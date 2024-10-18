using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PoePart2.Controllers;
using PoePart2.Data;
using PoePart2.Models;
namespace PoePart2.Tests;

[TestClass]
public class UnitTest1
{
    [TestClass]
    public class ClaimControllerTests
    {
        [TestMethod]
        public void Status_ReturnsViewWithClaims_ForLoggedLecturer()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(c => c.User.Identity.Name).Returns("Lecturer1");

            var controller = new ClaimController();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            // Add a test claim to ClaimStorage
            ClaimStorage.Claims.Add(new ClaimModel
            {
                ClaimId = 1,
                LecturerId = "Lecturer1",
                Amount = 100,
                HoursWorked = 10,
                Status = "Pending"
            });

            // Act
            var result = controller.Status() as ViewResult;

            // Assert
            var model = result.Model as IEnumerable<ClaimModel>;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count());
        }
    }

    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void Dashboard_ReturnsViewWithPendingClaims()
        {
            // Arrange
            var controller = new AdminController();

            // Add a test pending claim to ClaimStorage
            ClaimStorage.Claims.Add(new ClaimModel
            {
                ClaimId = 2,
                LecturerId = "Lecturer2",
                Amount = 200,
                Status = "Pending"
            });

            // Act
            var result = controller.Dashboard() as ViewResult;

            // Assert
            var model = result.Model as IEnumerable<ClaimModel>;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count());  // Verify that 1 pending claim is returned
            Assert.AreEqual("Pending", model.First().Status);  // Ensure the status is pending
        }
    }
}
