using System.Text;

namespace KernUx.Blazor.Utilities;

internal static class EnumCssExtensions
{
    public static string ToKebabCase<TEnum>(this TEnum value)
        where TEnum : struct, Enum
    {
        return ToKebabCase(value.ToString());
    }

    public static string ToKebabCase(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var builder = new StringBuilder(value.Length + 4);

        for (var index = 0; index < value.Length; index++)
        {
            var current = value[index];

            if (char.IsUpper(current))
            {
                var hasPrevious = index > 0;
                var previous = hasPrevious ? value[index - 1] : '\0';
                var hasNext = index < value.Length - 1;
                var next = hasNext ? value[index + 1] : '\0';

                if (hasPrevious && (char.IsLower(previous) || char.IsDigit(previous) || (char.IsUpper(previous) && hasNext && char.IsLower(next))))
                {
                    builder.Append('-');
                }

                builder.Append(char.ToLowerInvariant(current));
                continue;
            }

            builder.Append(current == '_' ? '-' : current);
        }

        return builder.ToString();
    }
}

