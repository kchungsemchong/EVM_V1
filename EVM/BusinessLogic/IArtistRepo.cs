using System.Collections.Generic;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public interface IArtistRepo
    {
        IEnumerable<Artist> Retrieve();
        Artist Get(int id);
        Artist Create(Artist item);
        Artist Update(Artist item);
    }
}