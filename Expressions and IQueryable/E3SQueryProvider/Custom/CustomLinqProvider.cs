using System;
using System.Linq;
using System.Linq.Expressions;

namespace Expressions.Task3.E3SQueryProvider.Custom;

public class CustomLinqProvider : IQueryProvider
{
    private readonly ProductDataSource _dataSource;

    public CustomLinqProvider(ProductDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        return new CustomQuery<TElement>(expression, this);
    }

    public object Execute(Expression expression)
    {
        throw new NotImplementedException();
    }

    public TResult Execute<TResult>(Expression expression)
    {
        var translator = new ExpressionToSqlTranslator();
        var sqlQuery = $"SELECT * FROM [dbo].[Products] {translator.Translate(expression)};";

        return (TResult)_dataSource.ExecuteSqlQuery(sqlQuery);
    }
}