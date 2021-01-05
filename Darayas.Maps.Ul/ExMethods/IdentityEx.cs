using Darayas.Maps.DAL.Models;
using Darayas.Maps.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Darayas.Maps.Ul.ExMethods
{
    public static class IdentityEx
    {
        public static async Task<tblUsers> GetUserDetailsAsync(this ClaimsPrincipal User)
        {
            var repUser = new SqlRepository<tblUsers>();
            try
            {
                var qUser = await repUser.Get()
                    .Where(a => a.UserName == User.Identity.Name)
                    .Select(a => new tblUsers
                    {
                        Email = a.Email,
                        Family = a.Family,
                        Id = a.Id,
                        Name = a.Name,
                        UserName = a.UserName
                    })
                    .SingleOrDefaultAsync();

                return qUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                repUser.Dispose();
            }
        }
    }
}
