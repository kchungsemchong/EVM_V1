using EVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVM.BusinessLogic
{
    public interface ISponsorRepo
    {
        IEnumerable<Sponsor> Retrieve();
        Sponsor Get(int id);
        Sponsor Create(Sponsor item, HttpPostedFileBase photo);
        Sponsor Update(Sponsor item);
    }
}