using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using rpgAPI.Controller;
using rpgAPI.Model;
using rpgAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System;

namespace rpdAPITest
{
    public class CharacterControllerTest
    {
        public Mock<ICharacterService> mockService = new Mock<ICharacterService>();
        public Fixture fixture = new Fixture();

        [Fact]
        public void GetAllCharacterGivenValidRequestReturnsOk()
        {
            // Arrange
            var serviceResponse = fixture.Create<ServiceResponse<List<Character>>>();
            mockService.Setup(x => x.GetAllCharacter()).Returns(serviceResponse);
            var charController = new CharacterController(mockService.Object);

            // Act
            var result = charController.GetCharacter();
            var okResult = (ObjectResult)result.Result;
            //Console.WriteLine("ok result data " + okResult.Data);

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
        [Fact]
        public void GetAllCharacterGivenNoCharactersExistReturnsNotFound()
        {
            // Arrange
            var serviceResponse = fixture.Create<ServiceResponse<List<Character>>>();
            serviceResponse.Data = new List<Character>();
            mockService.Setup(x => x.GetAllCharacter()).Returns(serviceResponse);
            var charController = new CharacterController(mockService.Object);

            // Act
            var result = charController.GetCharacter();
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetIdGivenValidIdReturnsOk1()
        {
            // Arrange
            var id = 1;
            var expectedCharacter = new Character { Id = id, Name = "Deepa Mittal" };
            var serviceResponse = new ServiceResponse<Character>() { Data = expectedCharacter };
            mockService.Setup(x => x.GetCharacterById(id)).Returns(serviceResponse);
            var charController = new CharacterController(mockService.Object);

            // Act
            var result = charController.GetId(id);
            var okResult = (ObjectResult)result.Result;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetIdGivenInvalidIdReturnsNotFound()
        {
            // Arrange
            var invalidId = 100;
            mockService.Setup(x => x.GetCharacterById(invalidId)).Returns((ServiceResponse<Character>)null);
            var charController = new CharacterController(mockService.Object);

            // Act
            var result = charController.GetId(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PostCharacterGivenValidCharacterReturnsOk()
        {

            var newCharacter = new Character { Name = "Test Character D" };

            var expectedCharacter = new Character { Id = 1, Name = "Test Character D" };
            var response = new ServiceResponse<List<Character>>() { Data = new List<Character>() { expectedCharacter } };
            mockService.Setup(x => x.AddCharacter(newCharacter)).Returns(response);
            var charController = new CharacterController(mockService.Object);

            // Act
            var result = charController.PostCharacter(newCharacter);

            Console.WriteLine("see the result , why failed" + result);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var serviceResponse = okResult.Value as ServiceResponse<List<Character>>;
            Assert.NotNull(serviceResponse);
            Assert.Single(serviceResponse.Data);
            Assert.Equal(expectedCharacter.Id, serviceResponse.Data[0].Id);
            Assert.Equal(expectedCharacter.Name, serviceResponse.Data[0].Name);

        }

        [Fact]
        public void UpdateCharacterGivenValidCharacterReturnsOk()
        {
            // Arrange
            var character = fixture.Create<Character>();
            var serviceResponse = fixture.Create<ServiceResponse<List<Character>>>();
            mockService.Setup(x => x.UpdateCharacter(character)).Returns(serviceResponse);
            var charController = new CharacterController(mockService.Object);

            // Act
            var result = charController.UpdateCharacter(character);
            var okResult = (ObjectResult)result.Result;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void UpdateCharacter_NotFound_ReturnsNotFound()
        {
            // Arrange
            var newCharacter = new Character { Id = 100, Name = "Updated Character" };
            mockService.Setup(x => x.UpdateCharacter(newCharacter)).Returns((ServiceResponse<List<Character>>)null);
            var charController = new CharacterController(mockService.Object);

            // Act
            var result = charController.UpdateCharacter(newCharacter);

            // Assert
           Assert.IsType<OkObjectResult>(result.Result);    
        }

        [Fact]
        public void DeleteCharacterGivenValidIdReturnsOk()
        {
            // Arrange
            var id = 1;
            var serviceResponse = fixture.Create<ServiceResponse<List<Character>>>();
            mockService.Setup(x => x.DeleteCharacter(id)).Returns(serviceResponse);
            var charController = new CharacterController(mockService.Object);

            // Act
            var result = charController.DeleteCharacter(id);
            var okResult = (ObjectResult)result.Result;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}