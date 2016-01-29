using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Platform.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Repository
{
    public class DataManager
    {
        public static void InitDataAccess()
        {
            IConfigurationSource source = System.Configuration.
                ConfigurationManager.GetSection("activerecord")
                as IConfigurationSource;

            ActiveRecordStarter.Initialize(source,
                typeof(Blog));
        }
    }
}
