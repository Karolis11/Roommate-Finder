using Microsoft.EntityFrameworkCore;
using roommate_app.Data;

public interface IGenericService
{
    Task<List<T>> GetAllAsync<T>() where T : class;
    T GetById<T>(object id) where T : class;
    Task AddAsync<T>(T obj) where T : class;
    Task UpdateAsync<T>(int id, T obj) where T : class;
    Task DeleteAsync<T>(object id) where T : class;
    Task SaveAsync();
}
public class GenericService : IGenericService
{ 
    private ApplicationDbContext _context;
    public GenericService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<T>> GetAllAsync<T>() 
        where T : class
    {
        return await _context.Set<T>().ToListAsync();
    }
    public T GetById<T>(object id)
        where T : class
    {
        return _context.Set<T>().Find(id);
    }
    public async Task AddAsync<T>(T obj)
        where T : class
    {
        await _context.Set<T>().AddAsync(obj);
        await SaveAsync();
    }
    public async Task UpdateAsync<T>(int id, T obj) 
        where T : class
    {
        var entity = _context.Set<T>().Find(id);
        if (entity == null)
        {
            return;
        }

        _context.Entry(entity).CurrentValues.SetValues(obj);
        await SaveAsync();
    }
    public async Task DeleteAsync<T>(object id)
        where T : class
    {
        T existing = await _context.Set<T>().FindAsync(id);
        _context.Set<T>().Remove(existing);
        await SaveAsync();
    }
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}