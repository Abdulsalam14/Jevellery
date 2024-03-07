using Jewellery.Core.DataAccess;
using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.DataAccess.Abstract
{
    public interface ICartDal:IEntityRepository<Cart>
    {
    }
}
