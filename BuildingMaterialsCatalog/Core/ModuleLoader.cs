using System.Reflection;

namespace BuildingMaterialsCatalog.Core
{
    public class ModuleLoader
    {
        public List<IModule> LoadModules()
        {
            var modules = new List<IModule>();

            var moduleTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t =>
                    typeof(IModule).IsAssignableFrom(t) &&
                    !t.IsInterface &&
                    !t.IsAbstract);

            foreach (var type in moduleTypes)
            {
                var module = (IModule)Activator.CreateInstance(type)!;
                modules.Add(module);
            }

            return modules;
        }
    }
}