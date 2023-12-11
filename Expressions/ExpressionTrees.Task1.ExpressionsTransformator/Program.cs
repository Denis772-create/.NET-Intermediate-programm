/*
 * Create a class based on ExpressionVisitor, which makes expression tree transformation:
 * 1. converts expressions like <variable> + 1 to increment operations, <variable> - 1 - into decrement operations.
 * 2. changes parameter values in a lambda expression to constants, taking the following as transformation parameters:
 *    - source expression;
 *    - dictionary: <parameter name: value for replacement>
 * The results could be printed in console or checked via Debugger using any Visualizer.
 */
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Expression Visitor for increment/decrement.");
        Console.WriteLine();

        // Sample exp: x + 1 - y
        var expression = Expression.Add(Expression.Parameter(typeof(int), "x"),
            Expression.Subtract(Expression.Constant(1), Expression.Parameter(typeof(int), "y")));

        // parameter replacements: { "x" -> 10, "y" -> 5 }
        var parameterReplacements = new Dictionary<string, object>
        {
            { "x", 10 },
            { "y", 5 }
        };

        var transformer = new IncDecExpressionVisitor(parameterReplacements);
        var transformedExpression = transformer.Visit(expression);

        Console.WriteLine($"Original Expression: {expression}");
        Console.WriteLine($"Transformed Expression: {transformedExpression}");
        Console.ReadLine();
    }
}