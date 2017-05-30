using System;

namespace ITOps.UIComposition.Mvc
{
    public sealed class RegisterAssemblyForUICompositionAttribute : Attribute
    {
        public RegisterAssemblyForUICompositionAttribute(string baseNamespace)
        {
            BaseNamespace = baseNamespace;
        }

        public string BaseNamespace { get; }
    }
}
