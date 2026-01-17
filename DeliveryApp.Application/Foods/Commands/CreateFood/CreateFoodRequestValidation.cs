using FluentValidation;

namespace DeliveryApp.Application.Foods.Commands.CreateFood;

public class CreateFoodRequestValidation : AbstractValidator<CreateFoodCommand>
{
    public CreateFoodRequestValidation()
    {
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name minimal length is 3.").MaximumLength(250)
            .WithMessage("Name maximum length is 250.");
        RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Description maximum length is 1000.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.FoodCategoryId).NotEmpty().WithMessage("Food category id cannot be empty.");
        
        RuleFor(x => x.Image)
            .Must(f => f.Length <= 5 * 1024 * 1024)
            .WithMessage("The image size cannot exceed 5MB.")
            .Must(f => new[] { ".jpg", ".jpeg", ".png" }
                .Contains(Path.GetExtension(f.FileName).ToLowerInvariant()))
            .WithMessage("Invalid image format. Use JPG or PNG.");
    }
}