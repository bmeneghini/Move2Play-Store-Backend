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
                Description = dto.Description
            };
        }
    }
}
