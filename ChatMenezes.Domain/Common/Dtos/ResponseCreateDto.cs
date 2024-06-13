using ChatMenezes.Core.Validator;

namespace ChatMenezes.Domain.Common.Dtos
{
    public class ResponseCreateDto<T> : Validator
    {
        public object Entity { get; set; }

        public override bool IsValid() => ValidationResult.IsValid;
    }
}
