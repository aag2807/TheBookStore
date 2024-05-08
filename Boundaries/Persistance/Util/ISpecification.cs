namespace Boundaries.Persistance.Util;

public interface ISpecification<TEntity> where TEntity : class
{
    public List<Criteria> Criterias { get; }

    public string? OrderBy { get; }

    public string? OrderByDescending { get; }

    List<string> IncludeStrings { get; }

    int? CurrentPage { get; }

    int? Take { get; }
}