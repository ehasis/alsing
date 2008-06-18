using System;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementName("Method")]
    [ElementIcon("GenerationStudio.Images.method.gif")]
    [ElementParent(typeof (InstanceTypeElement))]
    public class MethodElement : TypeMemberElement {}
}