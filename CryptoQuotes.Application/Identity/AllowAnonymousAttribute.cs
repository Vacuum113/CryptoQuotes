using System;

namespace Application.Identity
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}