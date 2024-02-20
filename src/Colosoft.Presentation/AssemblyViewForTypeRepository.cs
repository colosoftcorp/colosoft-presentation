using System;
using System.Collections.Generic;
using System.Linq;

namespace Colosoft.Presentation
{
    public class AssemblyViewForTypeRepository : IViewForTypeRepository
    {
        private readonly Dictionary<Type, Type> types = new Dictionary<Type, Type>();

        public AssemblyViewForTypeRepository(System.Reflection.Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var viewForType = typeof(IViewFor<>);

            foreach (var type in assembly.GetTypes())
            {
                var interfaces = type.GetInterfaces();

                foreach (var inter in interfaces)
                {
                    if (inter.IsGenericType && inter.GetGenericTypeDefinition() == viewForType)
                    {
                        var viewModelType = inter.GetGenericArguments().First();
                        this.types.Add(viewModelType, type);
                    }
                }
            }
        }

        public Type Get(Type viewModelType)
        {
            if (viewModelType is null)
            {
                throw new ArgumentNullException(nameof(viewModelType));
            }

            if (this.types.TryGetValue(viewModelType, out var type))
            {
                return type;
            }

            return null;
        }
    }
}
