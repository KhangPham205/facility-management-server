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

        // Xóa các tham số cấu hình mặc định rườm rà của Plainquire
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

        // Lấy các property public để tạo input filter
        var properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite && (p.PropertyType == typeof(string) || p.PropertyType.IsValueType));

        foreach (var prop in properties)
        {
            var paramName = prop.Name;

            if (!operation.Parameters.Any(p => p.Name.Equals(paramName, StringComparison.OrdinalIgnoreCase)))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = paramName,
                    In = ParameterLocation.Query,
                    Description = $"Filter by {prop.Name}. Use ~ for contains (e.g. ~abc), >10 for numbers.",
                    Schema = new OpenApiSchema { Type = "string" },
                    Required = false
                });
            }
        }
    }
}