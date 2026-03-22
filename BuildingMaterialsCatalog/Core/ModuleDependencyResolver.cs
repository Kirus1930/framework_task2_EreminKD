namespace BuildingMaterialsCatalog.Core
{
    public class ModuleDependencyResolver
    {
        public List<IModule> Resolve(List<IModule> modules)
        {
            var sorted = new List<IModule>();
            var visited = new HashSet<string>();
            var visiting = new HashSet<string>();

            var dictionary = modules.ToDictionary(m => m.Name);

            foreach (var module in modules)
            {
                Visit(module, dictionary, sorted, visited, visiting);
            }

            return sorted;
        }

        private void Visit(
            IModule module,
            Dictionary<string, IModule> dict,
            List<IModule> sorted,
            HashSet<string> visited,
            HashSet<string> visiting)
        {
            if (visited.Contains(module.Name))
                return;

            if (visiting.Contains(module.Name))
                throw new ModuleException($"Cycle detected in module dependencies at '{module.Name}'");

            visiting.Add(module.Name);

            foreach (var dependency in module.Dependencies)
            {
                if (!dict.ContainsKey(dependency))
                    throw new ModuleException($"Module '{module.Name}' requires missing module '{dependency}'");

                Visit(dict[dependency], dict, sorted, visited, visiting);
            }

            visiting.Remove(module.Name);
            visited.Add(module.Name);

            sorted.Add(module);
        }
    }
}