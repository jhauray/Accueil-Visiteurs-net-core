using System;
using back.net_core.v1.Controllers;
using Xunit;
using Moq;
using Microsoft.Extensions.Options;
using back.net_core.Models;
using Microsoft.AspNetCore.Mvc;

namespace back.netcore.Tests
{
    public class InfosControllerUnitTests
    {
        private readonly InfosController _controller;


        public InfosControllerUnitTests(){

            //Instanciation d'un Mock de l'objet InfoSettings
            //https://github.com/Moq/moq4/wiki/Quickstart
            //L'utilisation du Moq n'est pas indispensable ici. Il s'agit d'un exemple.
            var mockInfoSettings = new Mock<IOptions<InfoSettings>>();

            //Préparation de l'objet Setting "fake"
            var AssertInfoSettings = new InfoSettings();
            AssertInfoSettings.clouder = "AzureAWSIBM";
            AssertInfoSettings.playbook_url = "playbook_url";
            AssertInfoSettings.source_url = "source_url";

            //On indique au Mock que lorsque la valeur d'une option est consultée, il doit renvoyer l'objet "fake"
            mockInfoSettings.Setup(s=>s.Value).Returns(AssertInfoSettings);

            //Instanciation du contrôleur qui sera utilisé pour les tests.
            _controller = new InfosController(mockInfoSettings.Object);

        }
        
        [Fact]
        public void getInfos_All_filled()
        {
            //appel du contrôleur
            var result = _controller.getInfos();

            //Vérification que le type de la réponse est conforme
            Assert.IsType<ActionResult<BackInfo>>(result);

            //Vérification des valeurs
            Assert.Equal("AzureAWSIBM",result.Value.Clouder);
            Assert.Equal("playbook_url",result.Value.PlaybookUrl);
            Assert.Equal("source_url",result.Value.SourceUrl);

        }
    }
}
