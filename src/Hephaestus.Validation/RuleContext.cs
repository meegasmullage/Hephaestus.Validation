using System;
using System.Collections.Generic;

namespace Hephaestus.Validation
{
    public class RuleContext<T>
    {
        public T Instance { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        public List<string> Errors { get; private set; }

        internal RuleContext(IServiceProvider serviceProvider, T instance)
        {
            ServiceProvider = serviceProvider;
            Instance = instance;

            Errors = [];
        }
    }
}
