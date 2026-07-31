using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvToolkit;

namespace CsvToolKit;

public static class CsvSerializer
{
    
    //Maybe this can be a struct
    public class ColumnPlan
    {
        public PropertyInfo Property { get; set; } = null!;
        public string HeaderName { get; set; } = "";
        public int Order { get; set; }
    }

    public  static List<ColumnPlan> GetColumnPlan(Type type)
    {
        var plan = new List<ColumnPlan>();

        foreach (PropertyInfo prop in type.GetProperties())
        {
            if (Attribute.IsDefined(prop, typeof(CsvIgnoreAttribute)))
                continue;

            string headerName = prop.Name;
            int order = int.MaxValue;

            var attr = prop.GetCustomAttribute<CsvColumnAttribute>();
            if (attr != null)
            { 
                headerName = attr.Name;
                order = attr.Order;
            }

            plan.Add(new ColumnPlan
            {
                Property = prop,
                HeaderName = headerName,
                Order = order
            });
        }

        return plan.OrderBy(c => c.Order).ToList();
    }

    public static string WriteAll<T>(IEnumerable<T> items)
    {
        var plan = GetColumnPlan(typeof(T));
        var sb = new StringBuilder();


        List<string> headers = new List<string>();
        foreach (ColumnPlan col in plan)
        {
            headers.Add(EscapeCell(col.HeaderName));
        }
        sb.AppendLine(string.Join(",", headers));

        foreach (T item in items)
        {
            if (item == null) continue;

            var rowValues = new List<string>();
            foreach (var col in plan)
            {
                object? val = col.Property.GetValue(item);
                string strval = val?.ToString();
                
                rowValues.Add(EscapeCell(strval));
            }

            sb.AppendLine(string.Join(",", rowValues));
        }

        return sb.ToString();
    }

    private static string EscapeCell(string value)
    {
        if (string.IsNullOrEmpty(value)) return "";

        if (value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
        {
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        return value;
    }
}