using Ardalis.Specification.EntityFrameworkCore;
using Simple.Domain.Tenants;

namespace Simple.Infra.Database.Repositories;

public class Repository<T>(Db db) : RepositoryBase<T>(db), IRepository<T>
    where T : class, IAggregateRoot;