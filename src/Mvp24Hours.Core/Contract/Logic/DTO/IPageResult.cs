﻿namespace Mvp24Hours.Core.Contract.Logic.DTO
{
    /// <summary>
    /// Represents pagination results
    /// </summary>
    public interface IPageResult
    {
        /// <summary>
        /// Limit items on the page
        /// </summary>
        int Limit { get; }
        /// <summary>
        /// Page number or item block
        /// </summary>
        int Offset { get; }
        /// <summary>
        /// Quantity of items on the current page
        /// </summary>
        int Count { get; }
    }
}
