using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using back.net_core.Models;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Runtime.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace back.net_core.v1.Controllers{

    
    [ApiController]
    [ApiVersion( "1" )]
    [Route("v{version:apiVersion}/info")]
    public class InfosController : ControllerBase
    {
        //Paramètres d'infos
        private readonly IOptions<InfoSettings> _infoSettings;

        public InfosController(IOptions<InfoSettings> infoSettings){
            _infoSettings=infoSettings;
        }

        //GET /info
        [HttpGet]
        public ActionResult<BackInfo> getInfos()
        {
            BackInfo infos = new BackInfo();
            infos.Langage = "C#";

            //Lecture de la version .Net Core
            var framework = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
            infos.LangageVersion = framework ; 

            //Récupération des paramètres dans le ficheir de configuration
            infos.PlaybookUrl= _infoSettings.Value.playbook_url;
            infos.SourceUrl=_infoSettings.Value.source_url;
            infos.Clouder=_infoSettings.Value.clouder;

            //envoyer les infos
            return infos;
        }


    }
}