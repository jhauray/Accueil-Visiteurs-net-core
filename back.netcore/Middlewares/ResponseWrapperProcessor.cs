using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using back.net_core.Models;
using System.Collections.Generic;

namespace back.net_core.Middlewares
{
    public class ResponseWrapperProcessor
    {
        private readonly RequestDelegate _next;

        public ResponseWrapperProcessor(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                //Passage de la réponse au memorystream.
                context.Response.Body = memoryStream;

                await _next(context);

                //Réécriture du body 
                context.Response.Body = currentBody;
                memoryStream.Seek(0, SeekOrigin.Begin);

                var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                var objResult = JsonConvert.DeserializeObject(readToEnd);
                var result = new ResponseWrapper(objResult);
                
                //Ajout information sur l'erreur (code http hors plage 2xx)
                if(context.Response.StatusCode < StatusCodes.Status200OK 
                || context.Response.StatusCode >= StatusCodes.Status300MultipleChoices ){
                    //Instanciation de la liste si non définie.
                    if (result.Errors == null) result.Errors = new List<ErrorMessage>();
                    //Ajout du message avec le code erreur.
                    result.Errors.Add(new ErrorMessage(((HttpStatusCode)context.Response.StatusCode).ToString(),
                                        context.Response.StatusCode.ToString(),""));

                }

                //Ajout metadata (pagination)
                //TODO

                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }

    }

    
}