using System.Collections.Generic;
using System.Linq;

namespace Interview.FireworkStore.Core.Infrastructure
{
    public class ValidationResult
    {
        private readonly ICollection<string> _errors;

        public ValidationResult()
        {
            _errors = new List<string>();
        }

        public IEnumerable<string> Errors => _errors;

        public bool Success => !_errors.Any();

        public void AddError(string error)
        {
            _errors.Add(error);
        }
    }
}
