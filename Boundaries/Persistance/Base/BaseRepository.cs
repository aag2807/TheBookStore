using AutoMapper;
using Boundaries.Persistance.Context;
using Boundaries.Persistance.Extensions;
using Core.Boundaries.Persistance;
using Core.Boundaries.Persistance.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Triplex.Validations;

namespace Boundaries.Persistance.Base;

public abstract class BaseRepository<TEntity, TCore> : IBaseRepository<TCore> where TEntity : class where TCore : class
{
    private readonly IBookDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Represents the max page size for paged queries
    /// </summary>
    protected const int MaxPageSize = 25;

    /// <param name="context">An instance of <see cref="IBookDbContext"/></param>
    /// <param name="mapper">An instance of <see cref="IMapper"/></param>
    protected BaseRepository(IBookDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <inheritdoc cref="IBaseRepository{TCore}.GetPagedBySpec(ISpecification{TEntity}, string[])"/>
    public virtual async Task<(IEnumerable<TCore>, int)> GetPagedBySpec(ISpecification<TCore> specification, params string[] includes)
    {
        Arguments.NotNull(specification, nameof(specification));
        Arguments.NotNull(includes, nameof(includes));

        int elementsToSkip = (specification.CurrentPage!.Value - 1) * MaxPageSize;

        IQueryable<TEntity> query = AddIncludesToQuery(includes);

        query = query
            .OrderByPropertyName(specification.OrderBy, specification.OrderByDescending)
            .ApplyCriterias(specification.Criterias);

        int totalElements = await query.CountAsync();

        query = query
            .Skip(elementsToSkip)
            .Take(MaxPageSize);

        IEnumerable<TEntity> result = await query.ToListAsync();

        return (_mapper.Map<IEnumerable<TCore>>(result), totalElements);
    }

    /// <inheritdoc cref="IBaseRepository{TCore}.GetAllBySpec(ISpecification{TCore}, string[])"/>
    public virtual async Task<IEnumerable<TCore>> GetAllBySpec(ISpecification<TCore> specification, params string[] includes)
    {
        Arguments.NotNull(specification, nameof(specification));
        Arguments.NotNull(includes, nameof(includes));

        IQueryable<TEntity> query = AddIncludesToQuery(includes);

        query = query
            .OrderByPropertyName(specification.OrderBy, specification.OrderByDescending)
            .ApplyCriterias(specification.Criterias);

        IEnumerable<TEntity> result = await query.ToListAsync();

        return _mapper.Map<IEnumerable<TCore>>(result);
    }

    /// <inheritdoc cref="IBaseRepository{TCore}.GetByCriteriasAndThrow"/>
    public virtual async Task<TCore> GetByCriteriasAndThrow(IEnumerable<Criteria> criteria, params string[] includes)
    {
        Arguments.NotNull(criteria, nameof(criteria));
        Arguments.NotNull(includes, nameof(includes));

        IQueryable<TEntity> query = AddIncludesToQuery(includes);

        TEntity? result = await query.AsNoTracking().ApplyCriterias(criteria.ToList()).FirstOrDefaultAsync();

        State.IsFalse(result is null, "No se encontró el registro solicitado");

        return _mapper.Map<TCore>(result);
    }
    
    /// <inheritdoc cref="IBaseRepository{TCore}.GetByCriteriaAndThrow"/>
    public virtual async Task<TCore> GetByCriteriaAndThrow(Criteria criteria, params string[] includes)
    {
        Arguments.NotNull(criteria, nameof(criteria));
        Arguments.NotNull(includes, nameof(includes));

        IQueryable<TEntity> query = AddIncludesToQuery(includes);

        TEntity? result = await query.AsNoTracking().ApplyCriterias(new List<Criteria>(){ criteria }).FirstOrDefaultAsync();

        State.IsFalse(result is null, "No se encontró el registro solicitado");

        return _mapper.Map<TCore>(result);
    }
    
    /// <inheritdoc cref="IBaseRepository{TCore}.GetByCriteriaAndThrow"/>
    public virtual async Task<TCore> GetByCriteriaAndThrow(Criteria criteria, string errorMessage, params string[] includes)
    {
        Arguments.NotNull(criteria, nameof(criteria));
        Arguments.NotNull(includes, nameof(includes));
        Arguments.NotEmptyOrWhiteSpaceOnly(errorMessage, nameof(errorMessage));

        IQueryable<TEntity> query = AddIncludesToQuery(includes);

        TEntity? result = await query.AsNoTracking().ApplyCriterias(new List<Criteria>(){ criteria }).FirstOrDefaultAsync();

        State.IsFalse(result is null, errorMessage);

        return _mapper.Map<TCore>(result);
    }

    /// <inheritdoc cref="IBaseRepository{TCore}.GetByCriteriasAndThrow(System.Collections.Generic.IEnumerable{Core.Boundaries.Persistance.Util.Criteria},string,string[])"/>
    public virtual async Task<TCore> GetByCriteriasAndThrow(IEnumerable<Criteria> criteria, string errorMessage, params string[] includes)
    {
        Arguments.NotNull(criteria, nameof(criteria));
        Arguments.NotNull(includes, nameof(includes));
        Arguments.NotNullEmptyOrWhiteSpaceOnly(errorMessage, nameof(errorMessage));

        IQueryable<TEntity> query = AddIncludesToQuery(includes);

        TEntity? result = await query.AsNoTracking().ApplyCriterias(criteria.ToList()).FirstOrDefaultAsync();

        State.IsFalse(result is null, errorMessage);

        return _mapper.Map<TCore>(result);
    }

    /// <inheritdoc cref="IBaseRepository{TCore}.GetByCriterias"/>
    public virtual async Task<TCore?> GetByCriterias(IEnumerable<Criteria> criteria, params string[] includes)
    {
        Arguments.NotNull(criteria, nameof(criteria));

        IQueryable<TEntity> query = AddIncludesToQuery(includes);
        query = query.ApplyCriterias(criteria.ToList());

        TEntity? result = await query.AsNoTracking().FirstOrDefaultAsync();

        return result is null ? null : _mapper.Map<TCore>(result);
    }
    
    public virtual async Task<TCore?> GetByCriteria(Criteria criteria, params string[] includes)
    {
        Arguments.NotNull(criteria, nameof(criteria));
        Arguments.NotEmptyOrWhiteSpaceOnly(criteria.Value, nameof(Criteria.Value));
        Arguments.NotEmptyOrWhiteSpaceOnly(criteria.PropertyName, nameof(criteria.PropertyName));

        IQueryable<TEntity> query = AddIncludesToQuery(includes);
        query = query.ApplyCriterias(new List<Criteria> { criteria });

        TEntity? result = await query.AsNoTracking().FirstOrDefaultAsync();

        return result is null ? null : _mapper.Map<TCore>(result);
    }

    /// <inheritdoc cref="IBaseRepository{TCore}.ExistsByCriterias"/>
    public virtual async Task<bool> ExistsByCriterias(IEnumerable<Criteria> criteria, params string[] includes)
    {
        Arguments.NotNull(criteria, nameof(criteria));

        IQueryable<TEntity> query = _context.Set<TEntity>().ApplyCriterias(criteria.ToList());

        bool result = await query.AsNoTracking().AnyAsync();

        return result;
    }
    
    /// <inheritdoc cref="IBaseRepository{TCore}.ExistsByCriteria"/>
    public virtual async Task<bool> ExistsByCriteria(Criteria criteria, params string[] includes)
    {
        Arguments.NotNull(criteria, nameof(criteria));

        IQueryable<TEntity> query = _context.Set<TEntity>().ApplyCriterias(new List<Criteria> { criteria });

        bool result = await query.AsNoTracking().AnyAsync();

        return result;
    }

    /// <inheritdoc cref="IBaseRepository{TCore}.Create(TCore)"/>
    public virtual async Task<TCore> Create(TCore core)
    {
        Arguments.NotNull(core, nameof(core));

        TEntity dbModel = _mapper.Map<TEntity>(core);

        EntityEntry<TEntity> entry = await _context.Set<TEntity>().AddAsync(dbModel);

        await _context.SaveChangesAsync();

        TCore createdCore = _mapper.Map<TCore>(entry.Entity);

        return createdCore;
    }

    /// <inheritdoc cref="IBaseRepository{TCore}.Create(TCore)"/>
    public virtual async Task<IEnumerable<TCore>> CreateRange(IEnumerable<TCore> core)
    {
        Arguments.NotNull(core, nameof(core));

        IEnumerable<TEntity> dbModel = _mapper.Map<IEnumerable<TEntity>>(core);

        await _context.Set<TEntity>().AddRangeAsync(dbModel);

        await _context.SaveChangesAsync();

        return _mapper.Map<IEnumerable<TCore>>(dbModel);
    }

    /// <inheritdoc cref="IBaseRepository{TCore}.Update(TCore)"/>
    public virtual async Task<TCore> Update(TCore core)
    {
        Arguments.NotNull(core, nameof(core));

        TEntity dbModel = _mapper.Map<TEntity>(core);

        EntityEntry<TEntity> entry = _context.Set<TEntity>().Update(dbModel);

        await _context.SaveChangesAsync();

        TCore updatedCore = _mapper.Map<TCore>(entry.Entity);

        return updatedCore;
    }

    private IQueryable<TEntity> AddIncludesToQuery(string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }

    /// <inheritdoc />
    public virtual async Task DeleteRange(IEnumerable<TCore> core)
    {
        Arguments.NotNull(core, nameof(core));

        IEnumerable<TEntity> dbModel = _mapper.Map<IEnumerable<TEntity>>(core);

        _context.Set<TEntity>().RemoveRange(dbModel);

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public virtual async Task Delete(TCore core)
    {
        Arguments.NotNull(core, nameof(core));

        TEntity dbModel = _mapper.Map<TEntity>(core);

        _context.Set<TEntity>().Remove(dbModel);

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public virtual async Task BeginTransaction()
    {
        await _context.BeginTransaction();
    }

    /// <inheritdoc />
    public virtual async Task Commit()
    {
        await _context.Commit();
    }

    /// <inheritdoc />
    public virtual async Task RollBack()
    {
        await _context.RollBack();
    }
}