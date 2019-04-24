
using System;
using System.Collections.Generic;
using back.net_core.Models;

namespace back.net_core.Services
{
    public interface IVisiteService
    {
        void AddVisite(Visite newvisite);
        IEnumerable<Visite> GetAllVisites();

        void DeleteOlderVisite(DateTime daylimit);

        Visite GetOlderVisite();
    }
}