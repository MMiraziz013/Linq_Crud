namespace Clean.Application.Abstractions;

public interface IDataContext
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task MigrateAsync();
}