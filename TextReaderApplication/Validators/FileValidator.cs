namespace TextReaderApplication {
    using FluentValidation;

    public class FileValidator : AbstractValidator<string> {
        public FileValidator() {
            RuleFor(s => s).Must(IsValidFilePath).WithMessage("Please enter a valid file path");
            RuleFor(s => s).Must(IsValidFileType).WithMessage("Please select a valid .txt file");
        }

        /// <summary>
        /// Ensure that the file path is valid before trying to read it
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool IsValidFilePath(string filePath) {
            return File.Exists(filePath);
        }


        /// <summary>
        /// Ensure that a text file is being imported
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool IsValidFileType(string filePath) {
            string fileType = Path.GetExtension(filePath);
            if (fileType != ".txt") {
                return false;
            }

            return true;
        }
    }
}
