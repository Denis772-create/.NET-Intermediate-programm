using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer;

public class IncDecExpressionVisitor : ExpressionVisitor
{
    private readonly Dictionary<string, object> _parameterReplacements;

    public IncDecExpressionVisitor(Dictionary<string, object> parameterReplacements)
    {
        _parameterReplacements = parameterReplacements;
    }

    protected override Expression VisitBinary(BinaryExpression node)
    {
        // check if the binary expression is of the form: <variable> + 1 or <variable> - 1
        if (node.Right is ConstantExpression constant && Convert.ToInt32(constant.Value) == 1)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    return Expression.Increment(node.Left);
                case ExpressionType.Subtract:
                    return Expression.Decrement(node.Left);
            }
        }
        return base.VisitBinary(node);
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        // check if the parameter needs replacement
        if (_parameterReplacements.TryGetValue(node.Name, out var replacementValue))
        {
            return Expression.Constant(replacementValue, node.Type);
        }
        return base.VisitParameter(node);
    }
}