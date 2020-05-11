using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YangXuAPI.Data;

namespace YangXuAPI.Services
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly RoutineDbContext _routineDbContext;
        public CompanyRepository(RoutineDbContext routineDbContext)
        {
            _routineDbContext = routineDbContext;
        }
    }
}
