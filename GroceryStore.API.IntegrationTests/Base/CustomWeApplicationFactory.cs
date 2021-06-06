using GroceryStore.Domain.Entities;
using GroceryStore.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GroceryStore.API.IntegrationTests.Base
{
    public class CustomWeApplicationFactory<TStartup>
            : WebApplicationFactory<TStartup> where TStartup : class
    {       

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }   
}
