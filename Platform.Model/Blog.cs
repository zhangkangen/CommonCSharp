using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Model
{
    [ActiveRecord("Blogs")]
    public class Blog : BaseModel<Blog>
    {
        
    }
}
