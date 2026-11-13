using GymManagementSystem.DAL.Data.Context;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;

namespace GymManagementSystem.DAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _dbContext ;
        public IMemberRepository memberRepository { get; }

        public IHealthRecordRepository healthRecordRepository { get; }

        public IPlanRepository planRepository { get; }

        public ISessionRepository sessionRepository {  get; }

        public UnitOfWork(GymDbContext dbContext,IMemberRepository memberRepository,IHealthRecordRepository healthRecordRepository,IPlanRepository planRepository,ISessionRepository sessionRepository)
        {
            _dbContext = dbContext;
            this.memberRepository = memberRepository;
            this.healthRecordRepository = healthRecordRepository;
            this.planRepository = planRepository;
            this.sessionRepository = sessionRepository;
        }

        private readonly Dictionary<Type, object> Repos = [];

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var type = typeof(TEntity);

            if( Repos.TryGetValue(type,out object value))
            {
                return (IRepository<TEntity>) value;
            }

            var Repo = new Repository<TEntity>(_dbContext);


            Repos[type] = Repo;

            return Repo;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
