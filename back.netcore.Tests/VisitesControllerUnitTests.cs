using System;
using System.Collections.Generic;
using back.net_core.v1.Controllers;
using back.net_core.Models;
using back.net_core.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace back.netcore.Tests
{
    public class VisitesControllerUnitTests : IDisposable
    {
        private readonly VisitesController _controller;
        private readonly AccueilContext _context;
        
        public VisitesControllerUnitTests(){

            //Définition à la volée, d'un contexte de données InMemory, et non SQL Server
            var ctxOptions = new DbContextOptionsBuilder<AccueilContext>()
                                .UseInMemoryDatabase("Tests_Visites_database")
                                .Options;

            //Création du contexte
            _context = new AccueilContext(ctxOptions);

            //Ajout de visites dans le contexte
            var visites = new List<Visite>{
                new Visite {Hd=DateTime.Now,Id=1,Visiteur="Linus Torvalds"},
                new Visite {Hd=DateTime.Now,Id=2,Visiteur="Richard Stallman"}
            };
            _context.AddRange(visites);
            _context.SaveChanges();

            //Intanciation du controleur avec le contexte de base InMemory
            _controller = new VisitesController(new VisiteService(_context));           
        }

        public void Dispose()
        {
           //Vérification, suppression database si existe, pour repartir propre.
           _context.Database.EnsureDeleted();
        }

        [Fact]
        public void getVisitsWithoutError(){

            var result =  _controller.getVisits();

            //Vérification que le type de la réponse est conforme
            Assert.IsType<ActionResult<IEnumerable<Visite>>>(result);

            Assert.NotNull(result.Value);

            Assert.Equal(2,result.Value.Count());

        }

        [Fact]
        public void getVisitsOlddeleted(){

            //Modification des visites : changement de jour, passage à hier
            foreach(Visite v in _context.Visites){ v.Hd = v.Hd.AddDays(-1);}
            _context.SaveChanges();


            var result =  _controller.getVisits();


            //Vérification que le type de la réponse est conforme
            Assert.IsType<ActionResult<IEnumerable<Visite>>>(result);

            //Vérification réponse présente
            Assert.NotNull(result.Value);

            //Vérification que la liste a bien été purgée suite au changement de jour
            Assert.Equal(0,result.Value.Count());
        }


        [Fact]
        public void addVisitModelOK(){
            //Préparation d'une visite
            Visite visitToAdd = new Visite(){Hd=DateTime.Now,Id=4,Visiteur="Paul Allen"};

            //Ajout d'une visite
            StatusCodeResult result = _controller.addVisit(visitToAdd) as StatusCodeResult ;

            //Vérification que la réponse est bien 201 - CREATED
            Assert.Equal(201,result.StatusCode);

        }

        [Fact]
        public void addVisitModelKO(){
             //Préparation d'une visite
            Visite visitToAdd = new Visite(){Hd=DateTime.Now,Id=4,Visiteur="Paul Allen"};

            //Ajout d'une erreur de model
            _controller.ModelState.AddModelError("1","simulation objet mal structuré");

            //Ajout d'une visite
            var result = _controller.addVisit(visitToAdd);

            //Vérification que la réponse est bien 422 - Unprocessable entity
            Assert.IsType<UnprocessableEntityResult>(result);

        }

       
    }
}