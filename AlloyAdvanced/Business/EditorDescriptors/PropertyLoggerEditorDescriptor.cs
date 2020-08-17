using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;
using EPiServer.Shell.ObjectEditing;
using System.Diagnostics;
using EPiServer.Core;

namespace AlloyAdvanced.Business.EditorDescriptors
{
    [EditorDescriptorRegistration(TargetType = typeof(int?))]
    [EditorDescriptorRegistration(TargetType = typeof(string))]
    [EditorDescriptorRegistration(TargetType = typeof(PageReference))]
    [EditorDescriptorRegistration(TargetType = typeof(CategoryList))]
    public class PropertyLoggerEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            try
            {
                Debug.WriteLine(
                    $"Property: {metadata.PropertyName,-30} " + 
                    $"Display: {metadata.ShowForDisplay,-5} " +
                    $"Edit: {metadata.ShowForEdit,-5} " +
                    $"Tab: {metadata.GroupName,-14} " +
                    $"Order: {metadata.Order,3} " +
                    $"Parent: {(metadata.Parent.Model as IContent).Name,-20} " +
                    $"Type: {metadata.ModelType.Name,-15}");
            }
            catch { }
            base.ModifyMetadata(metadata, attributes);
        }
    }
}