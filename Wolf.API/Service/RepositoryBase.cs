using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Wolf.API.Infrastructure;
using Wolf.Core.ExtensionMethods;
using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

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
            if (GuidExtensions.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.Empty;
                entity.CreatedDateTime = _dateTimeProvider.OffsetNow;
                entity.CreatedBy = _userProvider.LoginName;
                await DbSet.AddAsync(entity);
            }
            else
            {
                entity.UpdatedDateTime = _dateTimeProvider.OffsetNow;
                entity.UpdatedBy = _userProvider.LoginName;
                DbSet.Update(entity);
            }
        }
        public async Task<T> SaveEntityAsync(T model)
        {
            EntityEntry<T> result;
            var entityExisted = await _dbContext.Set<T>().AnyAsync(o => o.Id == model.Id);
            if (entityExisted)
            {
                model.UpdatedDateTime = _dateTimeProvider.OffsetNow;
                model.UpdatedBy = _userProvider.LoginName;
                result = DbSet.Update(model);
            }
            else
            {
                model.Id = Guid.NewGuid();
                model.CreatedDateTime = _dateTimeProvider.OffsetNow;
                model.CreatedBy = _userProvider.LoginName;
                result = await DbSet.AddAsync(model);
            }
            await UnitOfWork.SaveAsync();
            return result.Entity;
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
            DbSet.RemoveRange(entity);            
        }
        public async Task DeleteSave(List<T> entity)
        {
            DbSet.RemoveRange(entity);
            await UnitOfWork.SaveAsync();
        }
    }
}
