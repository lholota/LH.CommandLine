using System.Collections.Generic;

namespace LH.CommandLine
{
    internal class ExecutablesCollection
    {
        private readonly IDictionary<string, IExecutable> _executables;

        public ExecutablesCollection()
        {
            _executables = new Dictionary<string, IExecutable>();
        }

        public void Add(string name, IExecutable executable)
        {
            name = GetUnifiedName(name);

            // TODO: Wrap duplicate key exception with custom

            _executables.Add(name, executable);
        }

        public bool TryFind(string name, out IExecutable executable)
        {
            var unifiedName = GetUnifiedName(name);

            return _executables.TryGetValue(unifiedName, out executable);
        }

        private string GetUnifiedName(string name)
        {
            return name.ToLowerInvariant();
        }
    }
}