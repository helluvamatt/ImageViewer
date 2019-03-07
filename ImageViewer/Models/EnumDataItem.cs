using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ImageViewer.Models
{
    internal class EnumDataItem
    {
        public static IEnumerable<EnumDataItem> FromEnum(Type enumType)
        {
            if (enumType == null) throw new ArgumentNullException(nameof(enumType));
            if (!enumType.IsEnum) throw new ArgumentException($"{enumType.Name} is not an Enum", nameof(enumType));
            foreach (var obj in Enum.GetValues(enumType))
            {
                var name = enumType.GetEnumName(obj);
                var descAttr = enumType.GetMember(name).First().GetCustomAttribute<DescriptionAttribute>();
                if (descAttr != null) name = descAttr.Description;
                yield return new EnumDataItem(obj, name);
            }
        }

        private EnumDataItem(object value, string name)
        {
            Value = value;
            Name = name;
        }

        public string Name { get; }

        public object Value { get; }
    }
}

