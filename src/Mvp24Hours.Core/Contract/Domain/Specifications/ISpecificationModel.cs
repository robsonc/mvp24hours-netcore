//=====================================================================================
// Developed by Kallebe Lins (https://github.com/kallebelins)
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================
namespace Mvp24Hours.Core.Contract.Domain.Specifications
{
    /// <summary>
    /// Specification for class models (entity, dto and valueobjects)
    ///  <see cref="Mvp24Hours.Core.Contract.Domain.Specifications.ISpecification{T}"/>
    /// </summary>
    public interface ISpecificationModel<T> : ISpecification<T>
    {
        /// <summary>
        /// Checks whether a model meets the specification
        /// </summary>
        /// <param name="candidate">Represents a class instance</param>
        /// <returns>true|false</returns>
        bool IsSatisfiedBy(T candidate);
    }
}
