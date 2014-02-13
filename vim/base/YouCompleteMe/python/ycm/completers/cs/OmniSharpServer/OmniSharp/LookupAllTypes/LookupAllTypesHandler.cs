using System.Collections.Generic;
using System.Linq;
using ICSharpCode.NRefactory.TypeSystem;
using OmniSharp.Solution;

namespace OmniSharp.LookupAllTypes
{
    public class LookupAllTypesHandler
    {
        private readonly ISolution _solution;

        public LookupAllTypesHandler(ISolution solution)
        {
            _solution = solution;
        }

        public LookupAllTypesResponse GetLookupAllTypesResponse()
        {
            var classes = new HashSet<string>();
            var interfaces = new HashSet<string>();
            foreach (var project in _solution.Projects)
            {
                var types = project.References.OfType<IUnresolvedAssembly>()
                                   .SelectMany(unresolvedRef => unresolvedRef.GetAllTypeDefinitions())
                                   .Union(project.ProjectContent.GetAllTypeDefinitions());

                foreach (var def in types)
                {
                    if (def.Kind == TypeKind.Interface)
                    {
                        interfaces.Add(def.Name);
                    }
                    else
                    {
                        classes.Add(def.Name);
                    }
                }
            }

            return new LookupAllTypesResponse
                {
                    Types = GetAllTypesAsString(classes),
                    Interfaces = GetAllTypesAsString(interfaces)
                };
        }

        string GetAllTypesAsString(HashSet<string> types)
        {
            // This causes a conflict with the vim keyword 'contains'
            types.Remove("Contains");

            return types.Aggregate("", (current, type) => current + type + " ");
        }
    }
}
