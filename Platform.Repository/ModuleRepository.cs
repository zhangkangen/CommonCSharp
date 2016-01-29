using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using NHibernate;
using Platform.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        public int Get()
        {
            Console.WriteLine("ok");
            return 1;
        }
    }
}
