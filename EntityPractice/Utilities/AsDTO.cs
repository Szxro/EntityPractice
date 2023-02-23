using DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models;

namespace EntityPractice.Utilities
{
    public static class AsDTO
    {
        /// <summary>
        /// More efficient way to map DTO (single DTO)
        /// </summary>
        /// <param name="movieTheater"></param>
        /// <returns> a Dto of MovieTheater</returns>
        public static MovieTheaterDTO AsDto(this MovieTheater movieTheater)
        {
            return new MovieTheaterDTO
            {
                Name = movieTheater.Name,
                Description = movieTheater.Description,
                Rating = movieTheater.Rating,
                Latitude = movieTheater.Location.X,
                Longitude = movieTheater.Location.Y
            };   
        }
    }
}
