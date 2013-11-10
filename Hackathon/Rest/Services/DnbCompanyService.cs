﻿using Hackathon.Rest.Endpoints;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using Newtonsoft.Json;

namespace Hackathon.Rest.Services
{
    public class DnbCompanyService : GenericService<DnbCompanyRequest>
    {
        public object Get(DnbCompanyRequest request)
        {
            
            //Sandbox
            //request.AuthToken = "OLtSGKF9hEIePfGG45cjgpSSLIAk2cQcTJkc7i8Saw0tTiODz2NAUdyGUsKOsNmIHm5Jw/SsbjoycpFfMyhPoJS2NsIhOFZWYwZsO+3dfXlzlBvoLkAxP020zScurR6wnSKozn6np6rLhpE0ejUVTvG8iKCHywP9dyPgg1vXRX1o1YbX8XMlSpa+VAWhJVAiRRNuMmIlATJLamrhxSTJ1jgydRbdZ80Jm4llCRzOQCb/9CHkyKrt0AgQckb/X6nsR80ydiOqFRavtRLjK3X+qgRX9DRcaHLcWPGRWhWt8MMBt11zijUswuk8Z4UaocY9ZA8nSUWdiebbcpBy0hHxuvDIG4dZByNRBkWc6EYNXHI=";
            //Prod
            var tok = Session["AuthToken"];
            if (tok != null)
            {
                request._AuthToken = (string)tok;
            }
            else
            {
                Response.Redirect("/");
            }
            
            PopulateParameters(request);

            var restClient = new RestClient("https://maxcvservices.dnb.com/V4.0");
            var restRequest = new RestRequest();

            restRequest.Method = Method.GET;
            restRequest.Resource = String.Format("/organizations?fraudscore=true{0}", BuildParameterURL(request));

            restRequest.AddHeader("Authorization", request._AuthToken);
            var response = restClient.Execute(restRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpException(500, string.Format("D&B Request Failed: {0}",response.Content));
            }
            //var responseObject = JsonConvert.DeserializeObject<Rootobject>(response.Content);
            return response.Content;
        }



    }
}