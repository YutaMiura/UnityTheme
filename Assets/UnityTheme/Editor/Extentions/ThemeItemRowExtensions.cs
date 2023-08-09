using System.Collections.Generic;
using System.Linq;

namespace UnityTheme.Editor.Extentions
{
    public static class ThemeItemRowExtensions
    {
        public static bool HasDuplicatedKey(this IEnumerable<ThemeItemRow> rows)
        {
            var themeItemRows = rows as ThemeItemRow[] ?? rows.ToArray();
            var distinctKeys = themeItemRows.Select(r => r.Key).Distinct();
            return distinctKeys.Count() != themeItemRows.Count();
        }
        
        public static bool HasDuplicatedKey(this ThemeItemRow[] rows)
        {
            var distinctKeys = rows.Select(r => r.Key).Distinct();
            return distinctKeys.Count() != rows.Count();
        }
    }
}