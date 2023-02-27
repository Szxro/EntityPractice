using DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models;

namespace EntityPractice.Utilities
{
    public static class MapDTO
    {
        /// <summary>
        /// More efficient way to map DTO (single DTO)
        /// </summary>
        /// <param name="movieTheater"></param>
        /// <returns> a Dto of MovieTheater</returns>
        public static MovieTheaterDTO MovieTheatherAsDto(this MovieTheater movieTheater)
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

        public static CinemaDTO CinemaAsDto(this Cinema cinema)
        {
            return new CinemaDTO
            {
                CinemaType = cinema.CinemaType,
                Price = cinema.Price,
                MovieTheater = cinema.MovieTheater.MovieTheatherAsDto(),
            };
        }
    }
}
