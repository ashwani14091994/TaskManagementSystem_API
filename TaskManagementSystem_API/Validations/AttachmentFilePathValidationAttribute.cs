using System.ComponentModel.DataAnnotations;
using System.IO;

public class AttachmentFilePathValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var filePath = value as string;

        if (string.IsNullOrEmpty(filePath))
        {
            return new ValidationResult("FilePath is required.");
        }

        // Example validation to check if file path has a valid extension
        var validExtensions = new[] { ".pdf", ".jpg", ".png", ".docx" };
        var extension = Path.GetExtension(filePath);
        if (Array.IndexOf(validExtensions, extension) < 0)
        {
            return new ValidationResult("Invalid file type.");
        }

        return ValidationResult.Success;
    }
}
