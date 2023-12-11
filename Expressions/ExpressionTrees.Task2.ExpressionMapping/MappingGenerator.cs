using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task2.ExpressionMapping;

public class MappingGenerator
{
    public Mapper<TSource, TDestination> Generate<TSource, TDestination>(Dictionary<string, string> fieldMappings)
    {
        var sourceParam = Expression.Parameter(typeof(TSource));
        var bindings = new List<MemberBinding>();

        foreach (var destinationProperty in typeof(TDestination).GetProperties())
        {
            if (fieldMappings.TryGetValue(destinationProperty.Name, out var sourcePropertyName))
            {
                var sourceProperty = typeof(TSource).GetProperty(sourcePropertyName);
                if (sourceProperty != null && sourceProperty.PropertyType == destinationProperty.PropertyType)
                {
                    var sourcePropertyAccess = Expression.Property(sourceParam, sourceProperty);
                    var binding = Expression.Bind(destinationProperty, sourcePropertyAccess);
                    bindings.Add(binding);
                }
            }
        }

        var memberInit = Expression.MemberInit(Expression.New(typeof(TDestination)), bindings);
        var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(memberInit, sourceParam);

        return new Mapper<TSource, TDestination>(mapFunction.Compile());
    }
}