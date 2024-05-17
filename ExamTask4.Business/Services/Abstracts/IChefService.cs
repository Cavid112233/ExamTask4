using ExamTask4.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTask4.Business.Services.Abstracts
{
    public interface IChefService
    {
        Task AddChef(Chef chef);
        void DeleteChef(int id);
        void UpdateChef(int id, Chef newChef);
        Chef GetChef(Func<Chef, bool>? predicate = null);
        List<Chef> GetAllChefs(Func<Chef, bool>? predicate = null);
    }
}
