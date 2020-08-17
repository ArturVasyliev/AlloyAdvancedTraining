using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;

namespace AlloyAdvanced.Business.EditorDescriptors
{
    [EditorDescriptorRegistration(TargetType = typeof(CategoryList))]
    public class HideCategoryEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata,
            IEnumerable<Attribute> attributes)
        {
            //if (metadata.PropertyName == "icategorizable_category")
            if (metadata.PropertyName == Global.SystemPropertyNames.Category)
            {
                if (metadata.Parent.Model is PageData)
                {
                    metadata.ShowForEdit = false;
                }
                //else if(metadata.Parent.Model is BlockData)
                //{
                //    metadata.GroupName = SystemTabNames.PageHeader;
                //}
            }
            base.ModifyMetadata(metadata, attributes);
        }
    }
}