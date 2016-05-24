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
        IEnumerable<LocationRepo> Retrieve();
        LocationRepo Get(int id);
        LocationRepo Create(LocationRepo item);
        LocationRepo Update(LocationRepo item);
    }
}