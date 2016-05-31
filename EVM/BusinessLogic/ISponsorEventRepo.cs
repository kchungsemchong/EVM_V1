using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public interface ISponsorEventRepo
    {
        List<Sponsor> StoreSponsorForEvent(Sponsor item);
        List<Sponsor> SaveSponsorInEvent(List<Sponsor> items, int eventId);
    }
}