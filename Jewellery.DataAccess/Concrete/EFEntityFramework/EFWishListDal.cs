using Jewellery.Core.DataAccess.EntityFramework;
using Jewellery.DataAccess.Abstract;
using Jewellery.Entities;
using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.DataAccess.Concrete.EFEntityFramework
{
    public class EFWishListDal:EntityFrameworkRepositoryBase<WishList,AppDbContext>,IWishListDal
    {
        public EFWishListDal(AppDbContext context):base(context) 
        {
            
        }
    }
}
