using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vantage.Domain;
using Vantage.Infra.Crosscutting;
using Vantage.Infra.Crosscutting.Collections;
using Vantage.Infra.Crosscutting.Extensions;
using Vantage.Infra.Crosscutting.Specifications;

namespace Vantage.Infra.Data
{
    public abstract class EFRepository<TEntity, TKey> : Repository, ISqlRepository<TEntity, TKey>
        where TEntity : class
    {
        public new IEFUnitOfWork UnitOfWork { get; private set; }

        protected EFRepository(IEFUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual TEntity Find(TKey id)
        {
            return UnitOfWork
                .Set<TEntity>()
                .Find(id);
        }

        public virtual ICollection<TEntity> Find()
        {
            return UnitOfWork
                .Set<TEntity>()
                .ToList();
        }

        public virtual ICollection<TEntity> Find(ISpecification<TEntity> specification)
        {
            return FindSpecific(specification)
                .ToList();
        }

        public virtual IPagedCollection<TEntity> Find(ISpecification<TEntity> specification, IPagination pagination)
        {
            Ensure.Argument.NotNull(pagination, nameof(pagination));

            return FindSpecific(specification)
                .PaginateList(pagination);
        }

        public virtual IPagedCollection<TEntity> Find(IPagination pagination) => Find(new TrueSpecification<TEntity>(), pagination);

        public virtual async Task<TEntity> FindAsync(TKey id)
        {
            return await UnitOfWork
                .Set<TEntity>()
                .FindAsync(id);
        }

        public virtual async Task<ICollection<TEntity>> FindAsync()
        {
            return await UnitOfWork
                .Set<TEntity>()
                .ToListAsync();
        }

        public virtual async Task<ICollection<TEntity>> FindAsync(ISpecification<TEntity> specification)
        {
            return await FindSpecific(specification)
                .ToListAsync();
        }

        public virtual async Task<IPagedCollection<TEntity>> FindAsync(IPagination pagination) => await FindAsync(new TrueSpecification<TEntity>(), pagination);

        public virtual async Task<IPagedCollection<TEntity>> FindAsync(ISpecification<TEntity> specification, IPagination pagination)
        {
            Ensure.Argument.NotNull(pagination, nameof(pagination));

            return await FindSpecific(specification)
                .PaginateListAsync(pagination);
        }

        public virtual bool Any()
        {
            return UnitOfWork
                .Set<TEntity>()
                .AsNoTracking()
                .Any();
        }

        public virtual bool Any(ISpecification<TEntity> specification) => FindSpecific(specification).Any();

        public virtual async Task<bool> AnyAsync()
        {
            return await UnitOfWork
                .Set<TEntity>()
                .AsNoTracking()
                .AnyAsync();
        }

        public virtual async Task<bool> AnyAsync(ISpecification<TEntity> specification) => await FindSpecific(specification).AnyAsync();

        public virtual void Add(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity));

            UnitOfWork
                .Set<TEntity>()
                .Add(entity);

            UnitOfWork.SaveChanges();
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            Ensure.Argument.NotNull(entities, nameof(entities));

            UnitOfWork
                .Set<TEntity>()
                .AddRange(entities);

            UnitOfWork.SaveChanges();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity));

            await UnitOfWork
                .Set<TEntity>()
                .AddAsync(entity);

            await UnitOfWork.SaveChangesAsync();
        }

        public virtual async Task AddAsync(IEnumerable<TEntity> entities)
        {
            Ensure.Argument.NotNull(entities, nameof(entities));

            await UnitOfWork
                .Set<TEntity>()
                .AddRangeAsync(entities);

            await UnitOfWork.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity));

            UnitOfWork
                .Set<TEntity>()
                .Update(entity);

            UnitOfWork.SaveChanges();
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            Ensure.Argument.NotNull(entities, nameof(entities));

            UnitOfWork
                .Set<TEntity>()
                .UpdateRange(entities);

            UnitOfWork.SaveChanges();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity));

            UnitOfWork
                .Set<TEntity>()
                .Update(entity);

            await UnitOfWork.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            Ensure.Argument.NotNull(entities, nameof(entities));

            UnitOfWork
                .Set<TEntity>()
                .UpdateRange(entities);

            await UnitOfWork.SaveChangesAsync();
        }

        public virtual void Remove(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity));

            UnitOfWork
                .Set<TEntity>()
                .Remove(entity);

            UnitOfWork.SaveChanges();
        }

        public virtual void Remove(IEnumerable<TEntity> entities)
        {
            Ensure.Argument.NotNull(entities, nameof(entities));

            UnitOfWork
                .Set<TEntity>()
                .RemoveRange(entities);

            UnitOfWork.SaveChanges();
        }

        public virtual void Remove(ISpecification<TEntity> specification)
        {
            Ensure.Argument.NotNull(specification, nameof(specification));

            var entities = UnitOfWork
                .Set<TEntity>()
                .Where(specification.SatisfiedBy())
                .ToList();

            UnitOfWork
                .Set<TEntity>()
                .RemoveRange(entities);

            UnitOfWork.SaveChanges();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity));

            UnitOfWork
                .Set<TEntity>()
                .Remove(entity);

            await UnitOfWork.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(IEnumerable<TEntity> entities)
        {
            Ensure.Argument.NotNull(entities, nameof(entities));

            UnitOfWork
                .Set<TEntity>()
                .RemoveRange(entities);

            await UnitOfWork.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(ISpecification<TEntity> specification)
        {
            Ensure.Argument.NotNull(specification, nameof(specification));

            var entities = UnitOfWork
                .Set<TEntity>()
                .Where(specification.SatisfiedBy())
                .ToList();

            UnitOfWork
                .Set<TEntity>()
                .RemoveRange(entities);

            await UnitOfWork.SaveChangesAsync();
        }

        private IQueryable<TEntity> FindSpecific(ISpecification<TEntity> specification)
        {
            Ensure.Argument.NotNull(specification, nameof(specification));

            return UnitOfWork.Set<TEntity>()
                .Where(specification.SatisfiedBy());
        }
    }
}
