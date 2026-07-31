using System;

namespace CsvToolkit;

// Used to customize header text and column order
[AttributeUsage(AttributeTargets.Property)]
public class CsvColumnAttribute : Attribute
{
    public string Name { get; }
    public int Order { get; set; } = int.MaxValue; // Default to max so unordered columns come last

    public CsvColumnAttribute(string name)
    {
        Name = name;
    }
}

// Marker attribute to skip a property during export/import
[AttributeUsage(AttributeTargets.Property)]
public class CsvIgnoreAttribute : Attribute
{
}