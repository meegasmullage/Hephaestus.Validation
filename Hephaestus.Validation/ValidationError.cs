using System.Collections.Generic;

namespace Hephaestus.Validation
{
    public class ValidationError
    {
        public string Target { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public IEnumerable<ValidationError> Children { get; set; }
    }
}
