
using System;
using System.Collections.Generic;
using System.Linq;
using back.net_core.Models;

namespace back.net_core.Services
{
    public class VisiteService : IVisiteService
    {

        private AccueilContext _context;

        public VisiteService(AccueilContext context){
            _context=context;
        }

        public void AddVisite(Visite newvisite)
        {
             //enregistrer la nouvelle visite
            _context.Visites.Add(newvisite);
            _context.SaveChanges();
        }

        public void DeleteOlderVisite(DateTime daylimit)
        {
           _context.Visites.RemoveRange(_context.Visites.Where(v=>v.Hd<daylimit));
           _context.SaveChanges();
        }

        public IEnumerable<Visite> GetAllVisites()
        {
            //Vérification si changement de jour depuis dernière insertion
            Visite olderv = GetOlderVisite();
            if (olderv!=null && olderv.Hd.Date < DateTime.Today){
                //Purge des vieilles visites
                DeleteOlderVisite(DateTime.Today);
            }

             //Renvoi de toutes les visites enregistrées en base de données
            return _context.Visites;
        }

        public Visite GetOlderVisite()
        {
           return _context.Visites.OrderBy(Visite=>Visite.Hd).FirstOrDefault();
        }
    }
}