using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;

namespace AlloyAdvanced.Business.EditorDescriptors
{
    [EditorDescriptorRegistration(
        TargetType = typeof(string),
        UIHint = Global.SiteUIHints.Embiggen)]
    public class EmbiggenEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(
            ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);

            metadata.EditorConfiguration.Add("style", "width: 600px");
        }
    }
}