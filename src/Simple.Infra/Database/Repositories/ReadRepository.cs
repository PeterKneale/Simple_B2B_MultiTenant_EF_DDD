using Ardalis.Specification.EntityFrameworkCore;
using Simple.Domain.Tenants;

namespace Simple.Infra.Database.Repositories;

public class ReadRepository<T>(Db db) : RepositoryBase<T>(db), IReadRepository<T>
    where T : class;