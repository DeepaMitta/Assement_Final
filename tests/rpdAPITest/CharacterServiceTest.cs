using rpgAPI.Model;
using rpgAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpdAPITest
{
    public class CharacterServiceTest
    {
        [Fact]
        public void GetAllCharacterGivenValidRequestReturnsResult()
        {
            // Arrange
            var characterService = new CharacterService();

            // Act
            var serviceResponse = characterService.GetAllCharacter();

            // Assert
            Assert.NotNull(serviceResponse);
            Assert.NotNull(serviceResponse.Data);
            Assert.NotEmpty(serviceResponse.Data);
        }

        [Fact]
        public void AddCharacterGivenValidCharacterReturnsUpdatedListWithAddedCharacter()
        {
            // Arrange
            var characterService = new CharacterService();
            var newCharacter = new Character { Id = 1, Name = "Deepa Mittal" };

            // Act
            var result = characterService.AddCharacter(newCharacter);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Contains(newCharacter, result.Data);
            Console.WriteLine("actual " + newCharacter.Id + "expected" + result.Data[0].Id);
            Assert.Equal("Deepa Mittal", result.Data[0].Name);
        }

        [Fact]
        public void GetCharacterById_ExistingId_ReturnsCharacter()
        {
            // Arrange
            var characterService = new CharacterService();
            int existingId = 1;

            // Act
            var result = characterService.GetCharacterById(existingId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(existingId, result.Data.Id);
        }

        [Fact]
        public void GetCharacterById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var cs = new CharacterService();
            int nonExistingId = 2;

            // Act
            var result = cs.GetCharacterById(nonExistingId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Id Doesn't Exist", result.Message);
        }

        [Fact]
        public void UpdateCharacterGivenExistingCharacterReturnsUpdatedListWithModifiedCharacter()
        {
            // Arrange
        
            var characterService = new CharacterService();
            var updatedCharacter = new Character { Id = 1, Name = "Deepa Mittal" };

            // Act
            var result = characterService.UpdateCharacter(updatedCharacter);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Contains(updatedCharacter, result.Data);
            Assert.Equal(updatedCharacter.Name, result.Data[0].Name);

        }

        [Fact]
        public void DeleteCharacterGivenExistingIdReturnsUpdatedListWithoutDeletedCharacter()
        {
            // Arrange
            var cs = new CharacterService();
            int existingId = 1;

            // Act
            var result = cs.DeleteCharacter(existingId);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.DoesNotContain(result.Data, c => c.Id == existingId);
        }
    }
}
