namespace Simple.Domain;


public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot;