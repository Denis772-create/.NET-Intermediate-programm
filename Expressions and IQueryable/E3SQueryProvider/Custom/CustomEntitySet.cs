using Expressions.Task3.E3SQueryProvider.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Expressions.Task3.E3SQueryProvider.Custom;

public class CustomEntitySet<T> : IQueryable<T>
{
    protected readonly Expression Expr;
    protected readonly IQueryProvider QueryProvider;

    public CustomEntitySet(ProductDataSource dataSource)
    {
        if (dataSource == null)
        {
            throw new ArgumentNullException(nameof(dataSource));
        }

        Expr = Expression.Constant(this);
        QueryProvider = new CustomLinqProvider(dataSource);
    }

    public Type ElementType => typeof(T);
    public Expression Expression => Expr;

    public IQueryProvider Provider => QueryProvider;

    public IEnumerator<T> GetEnumerator()
    {
        return QueryProvider.Execute<IEnumerable<T>>(Expr).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return QueryProvider.Execute<IEnumerable>(Expr).GetEnumerator();
    }
}