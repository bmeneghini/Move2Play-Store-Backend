using move2playstoreAPI.DataTransferObjects;
using move2playstoreAPI.Models;

namespace move2playstoreAPI.Controllers.Mappers
{
    public static class RatingMapper
    {
        public static Rating ConvertDtoToModel(RatingDto dto)
        {
            return new Rating
            {
                UserId = dto.UserId,
                GameId = dto.GameId,
                Evaluation = ConvertRatingToEnum(dto.Evaluation)
            };
        }

        public static RatingDto ConvertModelToDto(Rating model)
        {
            return new RatingDto
            {
                UserId = model.UserId,
                GameId = model.GameId,
                Evaluation = ConverterEnumToRating(model.Evaluation)
            };
        }

        private static string ConvertRatingToEnum(int rating)
        {
            switch (rating)
            {
                case -1:
                    return "bad";
                case 0:
                    return "average";
                case 1:
                    return "good";
                default:
                    return "good";
            }
        }

        private static int ConverterEnumToRating(string rating)
        {
            switch (rating)
            {
                case "bad":
                    return -1;
                case "average":
                    return 0;
                case "good":
                    return 1;
                default:
                    return 1;
            }
        }
    }
}
