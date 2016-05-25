using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public interface ILocationRepo
    {
        IEnumerable<Location> Retrieve();
        Location Get(int id);
        Location Create(Location item);
        Location Update(Location item);
    }
}