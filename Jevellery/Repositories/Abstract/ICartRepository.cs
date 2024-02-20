using Jevellery.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Jevellery.Repositories.Abstract
{
    public interface ICartRepository : IRepository<Cart>
    {
    }
}
