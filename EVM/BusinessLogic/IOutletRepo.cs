using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public interface IOutletRepo
    {
        IEnumerable<Outlet> Retrieve();
        Outlet Get(int id);
        Outlet Create(Outlet item);
        Outlet Update(Outlet item);
    }
}