using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public interface ISponsorRepo
    {
        IEnumerable<Sponsor> Retrieve();
        Sponsor Get(int id);
        Sponsor Create(Sponsor item);
        Sponsor Update(Sponsor item);
    }
}