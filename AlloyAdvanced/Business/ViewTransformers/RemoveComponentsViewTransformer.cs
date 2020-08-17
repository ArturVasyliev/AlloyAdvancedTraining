using EPiServer.Shell.ViewComposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace AlloyAdvanced.Business.ViewTransformers
{
    [ViewTransformer]
    public class RemoveComponentsViewTransformer : IViewTransformer
    {
        private string userName = "Alice";

        private string[] componentsToRemove = new string[]
        {
            "EPiServer.Cms.Shell.UI.Components.ProjectModeToolbarComponent",
            "EPiServer.Cms.Shell.UI.Components.RecentItems"
        };

        public int SortOrder => 0;

        public void TransformView(ICompositeView view, IPrincipal principal)
        {
            // build a list of components to remove
            var components = new List<IComponentMatcher>();
            BuildListRecursively(view.RootContainer, components);

            // remove the components for the specified user
            if (string.Equals(userName, principal.Identity.Name, StringComparison.OrdinalIgnoreCase))
            {
                view.RootContainer.RemoveComponentsRecursive(components,
                    notifyComponentOnRemoval: false);
            }
        }

        private void BuildListRecursively(IContainer container, List<IComponentMatcher> components)
        {
            foreach (IComponent component in container.Components)
            {
                IContainer childContainer = component as IContainer;
                if (childContainer != null)
                {
                    BuildListRecursively(childContainer, components);
                }
                else
                {
                    if (componentsToRemove.Any(item => string.Equals(item, 
                        component.DefinitionName, StringComparison.OrdinalIgnoreCase)))
                    {
                        AddComponent(components, component.DefinitionName);
                    }
                }
            }
        }

        private void AddComponent(List<IComponentMatcher> components, string definitionName)
        {
            var item = new ConfigurationComponentMatcher(definitionName);
            components.Add(item);
        }

        private class ConfigurationComponentMatcher : IComponentMatcher, IContainerMatcher
        {
            private readonly string definitionName;

            public ConfigurationComponentMatcher(string definitionName)
            {
                this.definitionName = definitionName;
            }

            public bool MatchesComponent(IComponent component)
            {
                return string.Equals(definitionName, 
                    component.DefinitionName, StringComparison.OrdinalIgnoreCase);
            }

            public bool MatchesContainer(IContainer container)
            {
                return true;
            }
        }
    }
}