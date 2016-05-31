using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public interface IArtistEventRepo
    {
        List<Artist> StoreArtistForEvent(Artist item);
        List<Artist> SaveArtistInEvent(List<Artist> items, int eventId);
    }
}