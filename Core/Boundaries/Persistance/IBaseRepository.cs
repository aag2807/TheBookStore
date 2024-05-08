using Core.Boundaries.Persistance.Util;

namespace Core.Boundaries.Persistance;

/// <summary>
/// Manages all operations between <see cref="Carrier"/> and persistence layer
/// </summary>
public interface IBaseRepository<TCore> where TCore : class
{
    /// <summary>
    /// Get all items paged and filtered by spec
    /// </summary>
    /// <param name="specification">An instance of <see cref="ISpecification{TEntity}"/></param>
    /// <param name="includes">All includes</param>
    /// <returns>A list of cores that contains all matched items paged 
    /// and an <see cref="int"/> that represents the total matched Elements</returns>
    public Task<(IEnumerable<TCore>, int)> GetPagedBySpec(ISpecification<TCore> specification, params string[] includes);

    /// <summary>
    /// Get all items filtered by spec
    /// </summary>
    /// <param name="specification">An instance of <see cref="ISpecification{TEntity}"/></param>
    /// <param name="includes">All includes</param>
    /// <returns>A list of cores that contains all matched items paged 
    /// and an <see cref="int"/> that represents the total matched Elements</returns>
    public Task<IEnumerable<TCore>> GetAllBySpec(ISpecification<TCore> specification, params string[] includes);

    /// <summary>
    /// Get first item by specifications criterias and thorws if not found
    /// </summary>
    /// <param name="criteria">Represents a list of instances of <see cref="Criteria"/> with all filters</param>
    /// <param name="includes">All includes paths to append related entities data</param>
    /// <returns>An instance of found element</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Task<TCore> GetByCriteriasAndThrow(IEnumerable<Criteria> criteria, params string[] includes);
    
    /// <summary>
    /// Get first item by a single Criteria and thorws if not found
    /// </summary>
    /// <param name="criteria">Represents an instance of <see cref="Criteria"/> with all filters</param>
    /// <param name="includes">All includes paths to append related entities data</param>
    /// <returns>An instance of found element</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Task<TCore> GetByCriteriaAndThrow(Criteria criteria, params string[] includes);
    
    /// <summary>
    /// Get first item by a single Criteria and thorws if not found a custom error message
    /// </summary>
    /// <param name="criteria">Represents an instance of <see cref="Criteria"/> with all filters</param>
    /// <param name="errorMessage">Message of error when the entity was no found</param>
    /// <param name="includes">All includes paths to append related entities data</param>
    /// <returns>An instance of found element</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Task<TCore> GetByCriteriaAndThrow(Criteria criteria, string errorMessage, params string[] includes);

    /// <summary>
    /// Get first item by specifications criteria and thorws if not found with a custom message
    /// </summary>
    /// <param name="criteria">Represents a list of instances of <see cref="Criteria"/> with all filters</param>
    /// <param name="errorMessage">Message of error when the entity was no found</param>
    /// <param name="includes">All includes paths to append related entities data</param>
    /// <returns>An instance of found element</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Task<TCore> GetByCriteriasAndThrow(IEnumerable<Criteria> criteria, string errorMessage, params string[] includes);

    /// <summary>
    /// Get first item by specifications criterias
    /// </summary>
    /// <param name="criteria">Represents a list of instances of <see cref="Criteria"/> with all filters</param>
    /// <param name="includes">All includes paths to append related entities data</param>
    /// <returns>An instance of found element</returns>
    public Task<TCore?> GetByCriterias(IEnumerable<Criteria> criteria, params string[] includes);
    
    /// <summary>
    /// Get first item by a single specificiation Criteria
    /// </summary>
    /// <param name="criteria">Represents an instance of <see cref="Criteria"/> with all filters</param>
    /// <param name="includes">All includes paths to append related entities data</param>
    /// <returns>An instance of found element</returns>
    public Task<TCore?> GetByCriteria(Criteria criteria, params string[] includes);

    /// <summary>
    /// Get first item by specification criterias
    /// </summary>
    /// <param name="criteria">Represents a list of instances of <see cref="Criteria"/> with all filters</param>
    /// <param name="includes">All includes paths to append related entities data</param>
    /// <returns>An instance of found element</returns>
    public Task<bool> ExistsByCriterias(IEnumerable<Criteria> criteria, params string[] includes);
    
    /// <summary>
    /// Checks if a given entity exists by a single criteria
    /// </summary>
    /// <param name="criteria">Represents an instance of <see cref="Criteria"/> with all filters</param>
    /// <param name="includes">A collection of strings which reflect the joining of entities</param>
    /// <returns></returns>
    public Task<bool> ExistsByCriteria(Criteria criteria, params string[] includes);

    /// <summary>
    /// Creates the core entity in persistence layer
    /// </summary>
    /// <param name="core">The instance of core entity that is going to be create</param>
    /// <returns>An instance of core entity that was created</returns>
    public Task<TCore> Create(TCore core);

    /// <summary>
    /// Creates the core entity in persistence layer
    /// </summary>
    /// <param name="core">The instance of core entity that is going to be create</param>
    /// <returns>An instance of core entity that was created</returns>
    public Task<IEnumerable<TCore>> CreateRange(IEnumerable<TCore> core);

    /// <summary>
    /// Updates the core entity in persistence layer
    /// </summary>
    /// <param name="core">The instance of core entity that is going to be updated</param>
    /// <returns>An instance of core entity that was created</returns>
    public Task<TCore> Update(TCore core);

    /// <summary>
    /// Removes a list of entites of core
    /// </summary>
    /// <param name="core">The list of instances of core entities that are going to be removed</param>
    public Task DeleteRange(IEnumerable<TCore> core);

    /// <summary>
    /// Removes the core entity in persistence layer
    /// </summary>
    /// <param name="core">The instance of primary key core entity that is going to be removed</param>
    public Task Delete(TCore core);

    /// <summary>
    /// Begin a transaction
    /// </summary>
    public Task BeginTransaction();

    /// <summary>
    /// Commit a transaction
    /// </summary>
    public Task Commit();

    /// <summary>
    /// RollBack a transaction
    /// </summary>
    public Task RollBack();
}