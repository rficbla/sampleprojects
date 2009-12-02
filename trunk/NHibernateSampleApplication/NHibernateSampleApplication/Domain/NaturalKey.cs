using System;

namespace NHibernateSampleApplication.Domain
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class NaturalKey : Attribute
    {
    }
}