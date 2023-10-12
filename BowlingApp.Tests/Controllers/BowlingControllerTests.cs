using System;
using BowlingApp.Controllers;
using BowlingApp.Domain.Interfaces;
using BowlingApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BowlingApp.Tests.Controllers
{
    [TestFixture]
    public class BowlingControllerTests
    {
        private BowlingController _controller;
        private Mock<IBowlingGame> _mockGame;

        [SetUp]
        public void Setup()
        {
            _mockGame = new Mock<IBowlingGame>();
            _controller = new BowlingController(_mockGame.Object);
        }

        /// <summary>
        /// checks if the Roll action returns an OkResult
        /// when provided with valid input of 21 times
        /// </summary>
        [Test]
        public void Roll_ValidInput_ReturnsOk()
        {
            // Arrange
            var mockGame = new Mock<IBowlingGame>();
            var controller = new BowlingController(mockGame.Object);
            var validRolls = new int[21];
            var inputModel = new RollInputModel { Rolls = validRolls };

            // Act
            var result = controller.Play(inputModel);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            mockGame.Verify(g => g.Roll(It.IsAny<int>()), Times.Exactly(21));
        }
        /// <summary>
        /// scenario where the number of rolls is not 21.
        /// </summary>
        [Test]
        public void Roll_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var invalidRolls = new int[20]; // Invalid input with 20 integers
            var input = new RollInputModel { Rolls = invalidRolls };

            // Act
            var result = _controller.Play(input);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        /// <summary>
        /// This test checks if the Roll action returns a BadRequestObjectResult when invalid pins are rolled.
        /// when rolls is more than 10 pins
        /// </summary>
        [Test]
        public void Roll_InvalidPins_ReturnsBadRequest()
        {
            // Arrange
            var invalidRolls = new int[21];
            invalidRolls[10] = 11; // Invalid roll with more than 10 pins
            var input = new RollInputModel { Rolls = invalidRolls };

            // Act
            var result = _controller.Play(input);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        /// <summary>
        /// with correct score 
        /// </summary>
        [Test]
        public void Play_RetrieveScore_ReturnsOkWithScore()
        {
            // Arrange
            var validRolls = new int[21]; // Valid input with 21 integers
            var inputModel = new RollInputModel { Rolls = validRolls, RetrieveScore = true };
            _mockGame.Setup(g => g.GetTotalScore()).Returns(150); // Mocked score

            // Act
            var result = _controller.Play(inputModel) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(150, result.Value); // Expected score
            _mockGame.Verify(g => g.GetTotalScore(), Times.Once);
        }
        /// <summary>
        /// It creates a mock game that throws an exception, 
        /// and it checks if the action returns an ObjectResult with a 500 status code, 
        /// </summary>

        [Test]
        public void Play_RetrieveScore_ReturnsInternalServerError()
        {
            // Arrange
            var validRolls = new int[21]; // Valid input with 21 integers
            var inputModel = new RollInputModel { Rolls = validRolls, RetrieveScore = true };
            _mockGame.Setup(g => g.GetTotalScore()).Throws(new Exception("Test exception"));

            // Act
            var result = _controller.Play(inputModel) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode); // Internal Server Error status code
        }
    }
}
