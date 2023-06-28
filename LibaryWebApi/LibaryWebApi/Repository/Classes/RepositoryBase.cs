using LibaryWebApi.Data;
using LibaryWebApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibaryWebApi.Repository.Classes
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        private readonly SchoolLibraryDbContext _context;
        private DbSet<T> Entity;
        public RepositoryBase(SchoolLibraryDbContext context)
        {
            _context = context;
            Entity = _context.Set<T>();
        }

        /// <summary>
        /// Returns all of the records of the database table.
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAllRecordsAsync()
        {
            return await Entity.ToListAsync();
        }
        /// <summary>
        /// Returns one record by provided id.
        /// </summary>
        /// <returns></returns>
        public async Task<T> GetRecordByIdAsync(int id)
        {
            T? record = await Entity.FindAsync(id);

            return record;
        }

        /// <summary>
        /// Inserts record to the database.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task CreateRecordAsync(T record)
        {
            if (record == null)
            {
                throw new ArgumentException("Record is not found!");
            }

            await Entity.AddAsync(record);
            await SaveAsync();
        }

        /// <summary>
        /// Updates provided record
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task UpdateRecordAsync(T record)
        {
            _context.Attach(record);
            var entry = _context.Entry(record);
            entry.State = EntityState.Modified;
            /*Entity.Attach(record);
            var entry = _context.Entry(record);
            entry.State = EntityState.Modified;*/

            await SaveAsync();
        }

        /// <summary>
        /// Removes the record
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task RemoveRecordAsync(T record)
        {
            Entity.Remove(record);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
