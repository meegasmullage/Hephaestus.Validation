using System;
using System.Collections.Generic;

namespace Hephaestus.Validation
{
    public class ValidationFailedException : Exception
    {
        public IEnumerable<ValidationError> Errors { get; private set; }

        internal ValidationFailedException(IEnumerable<ValidationError> errors) : base()
        {
            Errors = errors;
        }
    }
}
