using PM.Data.Entity;
using PM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Buisness.Repositories.IRepositories
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member> GetAsync(int id);
        Task<Member> GetAsync(string couponName);
        Task CreateAsync(Member coupon);
        Task UpdateAsync(Member coupon);
        Task<int> RemoveAsync(int id);
    }
}
