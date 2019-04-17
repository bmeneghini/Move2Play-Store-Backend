using move2playstoreAPI.DataTransferObjects;
using move2playstoreAPI.Models;
using System;

namespace move2playstoreAPI.Controllers.Mappers
{
    public static class GameMapper
    {
        public static Game ConvertDtoToModel(GameUploadDto dto)
        {
            return new Game()
            {
                Name = dto.Name,
                DeveloperId = dto.DeveloperId,
                Price = dto.Price,
                Description = dto.Description,
                ReleaseDate = DateTime.Now,
                Genre = ConvertGenderFieldToEnum(dto.Gender)
            };
        }

        public static GameDto ConvertModelToDto(Game game)
        {
            return new GameDto
            {
                Id = game.Id,
                ReleaseDate = game.ReleaseDate,
                Name = game.Name,
                Price = game.Price,
                Genre = game.Genre,
                ServerPath = game.ServerPath,
                Description = game.Description,
                Video = game.Video,
                Image =  game.Image,
                Comment =  game.Comment,
                Rating = game.Rating,
                DeveloperName = game.Developer.Name,
                Purchaseitem = game.Purchaseitem
            };
        }

        public static string ConvertGenderFieldToEnum(string gender)
        {
            switch (gender)
            {
                case "10":
                    return "Action";
                case "20":
                    return "Adventure";
                case "30":
                    return "Casual";
                case "40":
                    return "Sports";
                case "50":
                    return "Strategy";
                case "60":
                    return "Indie";
                default:
                    return "Casual";
            }
        }
    
    }
}
