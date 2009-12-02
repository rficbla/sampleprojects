using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NHibernateSampleApplication.Domain
{
    public static class AttributeExtensions
    {
        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(this T model, Type attributeType)
        {
            return model
                .GetType()
                .GetProperties()
                .Where(x => Attribute.IsDefined(x, attributeType, true));
        }
    }
}