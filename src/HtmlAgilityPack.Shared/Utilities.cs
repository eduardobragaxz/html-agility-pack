// Description: Html Agility Pack - HTML Parsers, selectors, traversors, manupulators.
// Website & Documentation: https://html-agility-pack.net
// Forum & Issues: https://github.com/zzzprojects/html-agility-pack
// License: https://github.com/zzzprojects/html-agility-pack/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.

using System.ComponentModel;

namespace HtmlAgilityPack;

internal static class Utilities
{
    public static TValue? GetDictionaryValueOrDefault<TKey, TValue>(Dictionary<TKey, TValue> dict, TKey key, TValue? defaultValue = default(TValue)) where TKey : class
    {
        if (!dict.TryGetValue(key, out TValue? value))
            return defaultValue;
        return value;
    }

#if !(METRO || NETSTANDARD1_3 || NETSTANDARD1_6)
    internal static object? To(this Object @this, Type type)
    {
        if (@this is not null)
        {
            Type targetType = type;

            if (@this.GetType() == targetType)
            {
                return @this;
            }

            TypeConverter converter = TypeDescriptor.GetConverterFromRegisteredType(@this);
            if (converter is not null)
            {
                if (converter.CanConvertTo(targetType))
                {
                    return converter.ConvertTo(@this, targetType);
                }
            }

            converter = TypeDescriptor.GetConverterFromRegisteredType(targetType);
            if (converter is not null)
            {
                if (converter.CanConvertFrom(@this.GetType()))
                {
                    return converter.ConvertFrom(@this);
                }
            }

            if (@this == DBNull.Value)
            {
                return null;
            }
        }

        return @this;
    }
#endif
}