using Jewellery.Core.DataAccess.EntityFramework;
using Jewellery.DataAccess.Abstract.Home;
using Jewellery.Entities;
using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.DataAccess.Concrete.EFEntityFramework.Home
{
    public class EFFirstContentDal:EntityFrameworkRepositoryBase<HomeFirstContent,AppDbContext>,IFIrstContentDal
    {
        public EFFirstContentDal(AppDbContext context):base(context)
        {
            
        }
    }
}
