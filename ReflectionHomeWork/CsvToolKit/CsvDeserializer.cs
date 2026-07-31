using System.Globalization;
using System.Text;
namespace CsvToolKit;
using static CsvSerializer;
public static class CsvDeserializer
{
    private static List<string> SplitCsvLine(string line)
    {
        //list that will store our final result
        var cells = new List<string>();
        StringBuilder currentCell = new StringBuilder();
        //tracks if cursor is currently inside double quotes.
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    currentCell.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                cells.Add(currentCell.ToString());
                currentCell.Clear();
            }
            else
            {
                currentCell.Append(c);
            }
        }

        cells.Add(currentCell.ToString());
        return cells;
    }
    
    private static object? ConvertValue(string text, Type targetType)
    {
        Type? underlyingType = Nullable.GetUnderlyingType(targetType);
        if (underlyingType != null)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            targetType = underlyingType;
        }

        if (targetType == typeof(string))
            return text;

        if (string.IsNullOrWhiteSpace(text))
            return null;

        if (targetType.IsEnum)
            return Enum.Parse(targetType, text, ignoreCase: true);

        if (targetType == typeof(Guid))
            return Guid.Parse(text);

        return Convert.ChangeType(text, targetType, CultureInfo.InvariantCulture);
    }
    public static List<T> ReadAll<T>(string csv) where T : new()
    {
        if (string.IsNullOrWhiteSpace(csv))
            return new List<T>();

        string[] lines = csv.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length <= 1) 
            return new List<T>();

        List<string> headers = SplitCsvLine(lines[0]);
        var headerIndexMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    
        for (int i = 0; i < headers.Count; i++)
        {
            headerIndexMap[headers[i]] = i;
        }

        List<CsvSerializer.ColumnPlan> plan = GetColumnPlan(typeof(T));
        List<T> results = new List<T>();

        for (int i = 1; i < lines.Length; i++)
        {
            List<string> cells = SplitCsvLine(lines[i]);
            T item = new T();

            foreach (CsvSerializer.ColumnPlan col in plan)
            {
                if (!col.Property.CanWrite)
                    continue;

                if (headerIndexMap.TryGetValue(col.HeaderName, out int cellIndex))
                {
                    if (cellIndex < cells.Count)
                    {
                        string rawCellText = cells[cellIndex];
                        object? value = ConvertValue(rawCellText, col.Property.PropertyType);
                        col.Property.SetValue(item, value);
                    }
                }
            }

            results.Add(item);
        }

        return results;
    }
}