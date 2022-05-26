using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Wolf.API.Infrastructure;
using Wolf.Core.Helpers;
using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Wolf.Core.Core;

namespace Wolf.API.Service
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : AuditEntity
    {
        private readonly DomainDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        protected DbSet<T> DbSet => _dbContext.Set<T>();

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _dbContext;
            }
        }

        public RepositoryBase(DomainDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserProvider userService)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;            
            _userProvider = userService;
        }
        public async Task AddOrUpdateAsync(T entity)
        {
            var existingItem = await _dbContext.Set<T>().FirstOrDefaultAsync(o => o.Id == entity.Id);
            if (existingItem != null)
            {
                entity.UpdatedDateTime = _dateTimeProvider.OffsetNow;
                entity.UpdatedBy = _userProvider.LoginName;
                _dbContext.Entry(existingItem).CurrentValues.SetValues(entity);
            }
            else
            {
                entity.Id = Guid.Empty;
                entity.CreatedDateTime = _dateTimeProvider.OffsetNow;
                entity.CreatedBy = _userProvider.LoginName;
                await _dbContext.Set<T>().AddAsync(entity);
            }
        }
        public async Task<T> SaveEntityAsync(T entity)
        {
            var existingItem = await _dbContext.Set<T>().FirstOrDefaultAsync(o => o.Id == entity.Id);
            if (existingItem != null)
            {
                entity.UpdatedDateTime = _dateTimeProvider.OffsetNow;
                entity.UpdatedBy = _userProvider.LoginName;
                _dbContext.Entry(existingItem).CurrentValues.SetValues(entity);
            }
            else
            {
                entity.Id = Guid.NewGuid();
                entity.CreatedDateTime = _dateTimeProvider.OffsetNow;
                entity.CreatedBy = _userProvider.LoginName;                
                await _dbContext.Set<T>().AddAsync(entity);
            }
            await UnitOfWork.SaveAsync();
            return entity;
        }
        public async Task<Paged<T>> GetPagedAsync(int page, int pageSize, int totalLimitItems, string search)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(search);
            }
            Paged<T> result = new Paged<T>(query, page, pageSize, totalLimitItems);
            result.Items = await query.Paged(page, pageSize, totalLimitItems).ToListAsync();
            return result;
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(o => o.Id == id);
        }
        public void Delete(List<T> entity)
        {
            DbSet.RemoveRange(DbSet.Where(o => entity.Select(o => o.Id).Contains(o.Id)));            
        }
        public async Task DeleteSave(List<T> entity)
        {
            DbSet.RemoveRange(DbSet.Where(o => entity.Select(o => o.Id).Contains(o.Id)));
            await UnitOfWork.SaveAsync();
        }
        public List<T> GetCategories()
        {
            List<string> columnNames = ReflectionUtil.GetColumnNameAttr<T>("category");
            if (columnNames.Count == 0)
                return null;
            string strColumns = ListHelpers.ConcatStrings(columnNames);
            var result = _dbContext.Set<T>().Select(LinQHelpers.DynamicSelectGenerator<T>(strColumns)).ToList();
            return result;
        }
    }
}
