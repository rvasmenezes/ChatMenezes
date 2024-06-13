using FluentValidation;
using FluentValidation.Results;

namespace ChatMenezes.Core.Validator
{
    public abstract class Validator
    {
        public ValidationResult ValidationResult { get; protected set; }

        protected Validator()
        {
            ValidationResult = new ValidationResult();
        }

        public void AddErrorValidation(int errorStatus, string description)
        {
            var failure = new ValidationFailure(errorStatus.ToString(), description)
            {
                Severity = Severity.Error,
                ErrorCode = errorStatus.ToString()
            };

            ValidationResult.Errors.Add(failure);
        }

        public void AddWarningValidation(string description)
        {
            var failure = new ValidationFailure("", description)
            {
                Severity = Severity.Warning
            };

            ValidationResult.Errors.Add(failure);
        }

        public abstract bool IsValid();

        public string? StatusDescription { get; set; }
    }
}
