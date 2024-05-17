using ExamTask4.Business.Exceptions;
using ExamTask4.Business.Extensions;
using ExamTask4.Business.Services.Abstracts;
using ExamTask4.Core.Models;
using ExamTask4.Core.RepositoryAbstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTask4.Business.Services.Concretes
{
    public class ChefService : IChefService
    {
        private readonly IChefRepository _chefRepository;
        private readonly IWebHostEnvironment _env;

        public ChefService(IChefRepository chefRepository, IWebHostEnvironment env)
        {
            _chefRepository = chefRepository;
            _env = env;
        }

        public async Task AddChef(Chef chef)
        {
            if (chef.ImageFile == null)
                throw new FileNullReferenceException("File bos ola bilmez!");
            chef.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\chefs", chef.ImageFile);

            await _chefRepository.AddAsync(chef);
            await _chefRepository.CommitAsync();
        }

        public void DeleteChef(int id)
        {
            var existChef = _chefRepository.Get(x=> x.Id == id);
            if (existChef == null) throw new EntityNotFoundException("Chef Tapilmadi");
            _chefRepository.Delete(existChef);
            _chefRepository.Commit();
        }

        public List<Chef> GetAllChefs(Func<Chef, bool>? predicate = null)
        {
            return _chefRepository.GetAll(predicate);
        }

        public Chef GetChef(Func<Chef, bool>? predicate = null)
        {
            return _chefRepository.Get(predicate);
        }

        public void UpdateChef(int id, Chef newChef)
        {
            var oldChef = _chefRepository.Get(x=>x.Id == id);
            if (oldChef == null)
                throw new EntityNotFoundException("Chef Tapilmadi");
            if(newChef.ImageFile != null)
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\chefs", oldChef.ImageUrl);
                oldChef.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\chefs", newChef.ImageFile);
            }
            oldChef.FullName = newChef.FullName;
            oldChef.Description = newChef.Description;
            oldChef.RedirectUrl = newChef.RedirectUrl;

            _chefRepository.Commit();
        }
    }
}
