using GymManagementSystem.DAL.Models;

namespace GymManagementSystem.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IMemberRepository memberRepository { get;  }
        public IHealthRecordRepository healthRecordRepository { get;  }
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity,new();

      public Task<int> SaveChangesAsync();
    }
}
