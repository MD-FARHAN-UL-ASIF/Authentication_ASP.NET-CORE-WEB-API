using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.iINTERFACES
{
    public interface IRepo<CLASS, ID, RET>
    {
        Task<List<CLASS>> Get();
        Task<CLASS> Get(ID id);
        Task<RET> Create(CLASS obj);
        Task<RET> Update(CLASS obj);
        Task<bool> Delete(ID id);
    }
}
