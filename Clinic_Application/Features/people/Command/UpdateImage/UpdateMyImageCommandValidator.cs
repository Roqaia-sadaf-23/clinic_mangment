using FluentValidation;

namespace Clinic_Application.Features.people.Command.UpdateImage
{
    public sealed class UpdateMyImageCommandValidator
    : AbstractValidator<UpdateImageCommand>
    {
        private static readonly string[] AllowedExtensions =
        {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    };

        public UpdateMyImageCommandValidator()
        {
            RuleFor(command => command.UserId)
                .GreaterThan(0);

            RuleFor(command => command.ImagePath)
                .NotEmpty()
                .MaximumLength(255)
                .Must(HaveAllowedExtension)
                .WithMessage("Invalid image extension.");
        }

        private static bool HaveAllowedExtension(
            string imagePath)
        {
            var extension = Path
                .GetExtension(imagePath)
                .ToLowerInvariant();

            return AllowedExtensions.Contains(extension);
        }
    }
}
