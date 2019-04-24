using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using back.net_core.Models;
using back.net_core.Services;

namespace back.net_core.v1.Controllers{

    
    [ApiController]
    [ApiVersion( "1" )]
    [Route("v{version:apiVersion}/visite")]
    public class VisitesController : Controller
    {
        //Service de données Visites
        private readonly IVisiteService _service;

        public VisitesController(IVisiteService service){
            _service = service;
        }

        //GET /visite
        [HttpGet]
        public ActionResult<IEnumerable<Visite>> getVisits()
        {
           return _service.GetAllVisites().ToArray();
        }


        //POST /visite (from body)
        [HttpPost]
        public ActionResult addVisit([FromBody] Visite value){
            
            //Vérificatinon résultat de la validation auto des paramètres du post...
            if (!ModelState.IsValid) {
                //Renvoi code 422 - Unprocessable entity
                return this.UnprocessableEntity();
                //return this.BadRequest(ModelState);
            }

            try
            {
                _service.AddVisite(value);
                
                //Retour code 201 - Created (il existe aussi la méthode Created, mais elle requière url et objet)
                return this.StatusCode(201);
            }
            catch (System.Exception)
            {
                //Erreur serveur - renvoi code 500 internal server error
                return this.StatusCode(500);
            }
           
        }
    }
}