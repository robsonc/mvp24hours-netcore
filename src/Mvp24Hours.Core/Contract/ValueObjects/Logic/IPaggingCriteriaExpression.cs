﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mvp24Hours.Core.Contract.ValueObjects.Logic
{
    /// <summary>
    /// Represents a definition for search criteria on a page
    /// </summary>
    public interface IPagingCriteriaExpression<T> : IPagingCriteria
    {
        /// <summary>
        /// Expression for sorting by ascending field
        /// </summary>
        IReadOnlyCollection<Expression<Func<T, dynamic>>> OrderByAscendingExpr { get; }
        /// <summary>
        /// Expression for sorting by descending field
        /// </summary>
        IReadOnlyCollection<Expression<Func<T, dynamic>>> OrderByDescendingExpr { get; }
        /// <summary>
        /// Expression for loading related objects
        /// </summary>
        IReadOnlyCollection<Expression<Func<T, dynamic>>> NavigationExpr { get; }
    }
}