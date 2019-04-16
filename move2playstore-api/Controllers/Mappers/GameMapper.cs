using move2playstoreAPI.DataTransferObjects;
using move2playstoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private static string ConvertGenderFieldToEnum(string gender)
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
