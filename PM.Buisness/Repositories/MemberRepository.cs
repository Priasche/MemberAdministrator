using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PM.Buisness.Repositories.IRepositories;
using PM.Data;
using PM.Data.Entity;
using PM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Buisness.Repositories
{
    public class MemberRepository:IMemberRepository
    {
        private readonly ApplicationDbContext db;

        public MemberRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(Member obj)
        {
            db.Member.Add(obj);
            await db.SaveChangesAsync();
        }


        public async Task<Member> GetAsync(string firstname)
        {
            return await db.Member.FirstOrDefaultAsync(u => u.FirstName.ToLower() == firstname.ToLower());
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            var obj = await db.Member.ToListAsync();
            return obj;
        }

        public async Task<Member> GetAsync(int id)
        {
            var obj = await db.Member.FirstOrDefaultAsync(u => u.Id == id);
            return obj;
        }

        public async Task<int> RemoveAsync(int id)
        {
            var obj = await db.Member.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                db.Member.Remove(obj);
                return await db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task UpdateAsync(Member obj)
        {
            var objFromDb = await db.Member.FirstOrDefaultAsync(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.FirstName = obj.FirstName;
                objFromDb.LastName = obj.LastName;
                objFromDb.Email = obj.Email;

                db.Member.Update(objFromDb);
                await db.SaveChangesAsync();
            }
        }
    }
}
