using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public interface IEventRepo
    {
        IEnumerable<Event> Retrieve();
        Event Get(int id);
        Event Create(Event item);
        Event Update(Event item);
    }
}