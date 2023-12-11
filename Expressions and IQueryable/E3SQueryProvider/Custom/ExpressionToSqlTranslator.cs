using System.Linq;
using System;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider.Custom;

public class ExpressionToSqlTranslator : ExpressionVisitor
{
    readonly StringBuilder _sqlQueryBuilder;

    public ExpressionToSqlTranslator()
    {
        _sqlQueryBuilder = new StringBuilder();
    }

    public string Translate(Expression exp)
    {
        Visit(exp);

        return _sqlQueryBuilder.ToString();
    }

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        if (node.Method.DeclaringType == typeof(Queryable)
            && node.Method.Name == "Where")
        {
            _sqlQueryBuilder.Append("WHERE ");
            var predicate = node.Arguments[1];
            Visit(predicate);

            return node;
        }

        return base.VisitMethodCall(node);
    }

    protected override Expression VisitBinary(BinaryExpression node)
    {
        switch (node.NodeType)
        {
            case ExpressionType.GreaterThan:
                Visit(node.Left);
                _sqlQueryBuilder.Append(" > ");
                Visit(node.Right);
                break;

            case ExpressionType.LessThan:
                Visit(node.Left);
                _sqlQueryBuilder.Append(" < ");
                Visit(node.Right);
                break;

            case ExpressionType.Equal:
                Visit(node.Left);
                _sqlQueryBuilder.Append(" = ");
                Visit(node.Right);
                break;

            case ExpressionType.AndAlso:
                Visit(node.Left);
                _sqlQueryBuilder.Append(" AND ");
                Visit(node.Right);
                break;

            default:
                throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
        };

        return node;
    }

    protected override Expression VisitMember(MemberExpression node)
    {
        _sqlQueryBuilder.Append(node.Member.Name);

        return base.VisitMember(node);
    }

    protected override Expression VisitConstant(ConstantExpression node)
    {
        if (node.Value is string)
            _sqlQueryBuilder.Append($"'{node.Value}'");
        else
            _sqlQueryBuilder.Append(node.Value);

        return node;
    }

}