using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public interface ITariffRepo
    {
        IEnumerable<Tariff> Retrieve();
        Tariff Get(int id);
        Tariff Create(Tariff item);
        Tariff Update(Tariff item);
    }
}