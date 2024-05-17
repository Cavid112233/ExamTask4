using ExamTask4.Core.Models;
using ExamTask4.Core.RepositoryAbstract;
using ExamTask4.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTask4.Data.RepositoryConcretes
{
    public class ChefRepository : GenericRepository<Chef>, IChefRepository
    {
        public ChefRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
