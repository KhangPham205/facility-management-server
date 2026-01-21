using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Plainquire.Filter;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

public class PlainquireFilterOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var filterParam = context.MethodInfo.GetParameters()
            .FirstOrDefault(p => p.ParameterType.IsGenericType &&
                                 p.ParameterType.GetGenericTypeDefinition() == typeof(EntityFilter<>));

        if (filterParam == null) return;

        var paramsToRemove = operation.Parameters
            .Where(p => p.Name.StartsWith("Configuration.") ||
                        p.Name.StartsWith("Interceptor.") ||
                        p.Name.Contains("FilterOperatorMap"))
            .ToList();

        foreach (var p in paramsToRemove)
        {
            operation.Parameters.Remove(p);
        }

        var entityType = filterParam.ParameterType.GetGenericArguments()[0];

        var properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite && (p.PropertyType == typeof(string) || p.PropertyType.IsValueType));

        foreach (var prop in properties)
        {
            var paramName = char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1);

            if (!operation.Parameters.Any(p => p.Name.Equals(paramName, StringComparison.OrdinalIgnoreCase)))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = paramName,
                    In = ParameterLocation.Query,
                    Description = $"Filter by {prop.Name} (e.g. 'contains=abc', '>10')",
                    Schema = new OpenApiSchema { Type = "string" }, // Filter Plainquire luôn nhận string
                    Required = false
                });
            }
        }
    }
}