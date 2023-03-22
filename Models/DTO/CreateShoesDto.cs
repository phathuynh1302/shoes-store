using FluentValidation;
using Microsoft.AspNetCore.Http;
using PRN211_ShoesStore.Models.Entity;
using System;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Models.DTO
{
    public class CreateShoesDto
    {
        public int id { get; set; }

        public string Name
        {
            get; set;
        }
        public string ShoesDetails
        {
            get; set;
        }

        public DateTime LaunchDate { get; set; }
        public String ShoesImage { get; set; }

        public decimal Price { get; set; }

        public IFormFile File
        {
            get; set;
        }

        public List<Color> Colors { get; set; }
        public List<Category> Categories { get; set; }

    }

    public class CreateRewardDtoValidator : AbstractValidator<CreateShoesDto>
    {
        public CreateRewardDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Price).NotEmpty().NotNull();
            RuleFor(x => x.File).NotNull();
            RuleFor(x => x.LaunchDate).NotEmpty().NotNull();
        }
    }
}
