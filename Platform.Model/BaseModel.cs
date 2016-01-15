using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Model
{
    public class BaseModel<T, TKey> : ActiveRecordBase<T>
    {
        public virtual TKey Id { get; set; } 
    }

    public class BaseModel<T> where T : BaseModel<T, Int32>
    {

    }
}
