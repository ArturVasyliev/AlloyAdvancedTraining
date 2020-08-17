using EPiServer.Shell.ViewComposition;
using System;
using System.Diagnostics;
using System.Security.Principal;

namespace AlloyAdvanced.Business.ViewTransformers
{
    [ViewTransformer]
    public class DebugViewTransformer : IViewTransformer
    {
        public int SortOrder => 0;

        public void TransformView(ICompositeView view, IPrincipal principal)
        {
            Debug.WriteLine($"View: {view.Name}, Title: {view.Title}, {DateTime.Now}");
            ScanViewRecursively(view.RootContainer, "  ");
        }
        private void ScanViewRecursively(IContainer container, string indent)
        {
            Debug.WriteLine($"{indent}Container: {container.DefinitionName}, {container.WidgetType}");

            indent += "  ";

            foreach (IComponent component in container.Components)
            {
                // if this component is a container then scan it
                IContainer containerComponent = component as IContainer;
                if (containerComponent != null)
                {
                    ScanViewRecursively(containerComponent, indent);
                }
                else
                {
                    Debug.WriteLine($"{indent}Component: {component.DefinitionName}, {component.WidgetType}");
                }
            }
        }
    }
}