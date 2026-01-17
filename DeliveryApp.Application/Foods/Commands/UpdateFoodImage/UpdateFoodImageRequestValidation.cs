using FluentValidation;

namespace DeliveryApp.Application.Foods.Commands.UpdateFoodImage;

public class UpdateFoodImageRequestValidation : AbstractValidator<UpdateFoodImageCommand>
{
    public UpdateFoodImageRequestValidation()
    {
        RuleFor(x => x.Image)
            .Must(f => f.Length <= 5 * 1024 * 1024)
            .WithMessage("The image size cannot exceed 5MB.")
            .Must(f => new[] { ".jpg", ".jpeg", ".png" }
                .Contains(Path.GetExtension(f.FileName).ToLowerInvariant()))
            .WithMessage("Invalid image format. Use JPG or PNG.");
    }
}