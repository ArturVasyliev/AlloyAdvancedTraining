using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;

namespace AlloyAdvanced.Business.EditorDescriptors
{
    [EditorDescriptorRegistration(TargetType = typeof(int?))]
    public class MoveSortIndexEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata,
            IEnumerable<Attribute> attributes)
        {
            // you must modify Global.cs to add the SortIndex string constant
            // or compare to the literal string "PagePeerOrder"
            if (metadata.PropertyName == Global.SystemPropertyNames.SortIndex)
            {
                metadata.GroupName = EPiServer.DataAbstraction.SystemTabNames.Content;
            }

            base.ModifyMetadata(metadata, attributes);
        }
    }
}