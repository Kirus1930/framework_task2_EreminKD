namespace ModularApp.Core
{
    public class ModuleDependencyResolver
    {
        public List<IModule> Resolve(List<IModule> modules)
        {
            var sorted = new List<IModule>();
            var visited = new HashSet<string>();
            var visiting = new HashSet<string>();
            var dict = modules.ToDictionary(m => m.Name);

            foreach (var m in modules)
                Visit(m, dict, sorted, visited, visiting);

            return sorted;
        }

        private void Visit(IModule m, Dictionary<string, IModule> dict,
            List<IModule> sorted, HashSet<string> visited, HashSet<string> visiting)
        {
            if (visited.Contains(m.Name)) return;
            if (visiting.Contains(m.Name))
                throw new ModuleException("Cycle detected");

            visiting.Add(m.Name);

            foreach (var dep in m.Dependencies)
            {
                if (!dict.ContainsKey(dep))
                    throw new ModuleException($"Missing module: {dep}");
                Visit(dict[dep], dict, sorted, visited, visiting);
            }

            visiting.Remove(m.Name);
            visited.Add(m.Name);
            sorted.Add(m);
        }
    }
}
