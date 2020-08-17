using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlloyAdvanced.Business.EditorDescriptors
{
    // this class will execute for ALL content types
    [EditorDescriptorRegistration(TargetType = typeof(ContentData))]
    public class MoveSortSubpagesEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata,
            IEnumerable<Attribute> attributes)
        {
            // you must modify Global.cs to add the SortIndex string constant
            // or compare to the literal string "PageChildOrderRule"
            var property = metadata.Properties.FirstOrDefault(
                p => p.PropertyName == Global.SystemPropertyNames.SortSubpages)
                as ExtendedMetadata;

            if (property != null)
            {
                property.GroupName = SystemTabNames.Content;
            }

            base.ModifyMetadata(metadata, attributes);
        }
    }
}