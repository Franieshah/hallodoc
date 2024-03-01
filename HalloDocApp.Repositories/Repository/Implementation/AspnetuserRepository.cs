using HalloDocApp.Entities.DataContext;
using HalloDocApp.Entities.DataModels;
using HalloDocApp.Repositories.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocApp.Repositories.Repository.Implementation
{
    public class AspnetuserRepository : GenericRepository<Aspnetuser>, IAspnetuserRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public AspnetuserRepository(ApplicationDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public Aspnetuser GetById(int id)
        {
            return _dbContext.Aspnetusers.Find(id);
        }
    }
}
