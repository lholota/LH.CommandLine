using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LH.CommandLine.Options2.Validation
{
    internal class ValidationResult : IEnumerable<string>
    {
        private readonly List<string> _errors;

        public ValidationResult()
        {
            _errors = new List<string>();

            IsValid = true;
        }

        public bool IsValid { get; private set; }

        public void AddError(string error)
        {
            _errors.Add(error);
            IsValid = false;
        }

        public void Merge(ValidationResult validationResult)
        {
            if (validationResult._errors.Any())
            {
                _errors.AddRange(validationResult._errors);

                IsValid = false;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}