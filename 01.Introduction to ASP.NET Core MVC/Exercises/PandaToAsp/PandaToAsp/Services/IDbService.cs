using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Panda.Data;

namespace PandaToAsp.Services
{
    public interface IDbService
    {
        ApplicationDbContext Db();
    }
}
