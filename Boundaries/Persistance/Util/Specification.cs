namespace Boundaries.Persistance.Util;

public class Specification<TEntity> : ISpecification<TEntity> where TEntity : class
{
    public Specification(string orderBy, string orderByDescending, List<Criteria>? criteria = null, int? currentPage = null)
    {
        OrderBy = !string.IsNullOrWhiteSpace(orderBy) ? orderBy : null;
        OrderByDescending = !string.IsNullOrWhiteSpace(orderByDescending) ? orderByDescending : null;
        Criterias = criteria ?? new List<Criteria>();
        CurrentPage = currentPage;
    }

    public List<Criteria> Criterias { get; }
    public string? OrderBy { get; }

    public string? OrderByDescending { get; }

    public List<string> IncludeStrings { get; } = new();

    public int? CurrentPage { get; }

    public int? Take { get; }

    public virtual void AddInclude(string includeString) => IncludeStrings.Add(includeString);
}