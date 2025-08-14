using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; private set; }
        public string? ErrorMessage { get; private set; }
        public DateTime? ProdDateTime { get; private set; }
        public string ExtraMessage { get; private set; } = "";

        public static ValidationResult Fail(string errorMessage) =>
            new ValidationResult { IsValid = false, ErrorMessage = errorMessage };

        public static ValidationResult Success(DateTime dateTime, string extraMessage) =>
            new ValidationResult { IsValid = true, ProdDateTime = dateTime, ExtraMessage = extraMessage };
    }
}
