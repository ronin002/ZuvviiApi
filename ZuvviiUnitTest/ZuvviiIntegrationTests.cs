using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;

using ZuvviiAPI.Models;
using NUnit.Framework;
using ZuvviiAPI.Dtos;
using ZuvviiAPI.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ZuvviiUnitTest
{
    public class ZuvviiIntegrationTests : IClassFixture<ZuvviiApiApplication>
    { 
        readonly HttpClient _client;

        public ZuvviiIntegrationTests(ZuvviiApiApplication apiApplication)
        {
            _client = apiApplication.CreateClient();
        }

        [Test]
        public async Task GET_ReturnUser_ById()
        {
            await using var application = new ZuvviiApiApplication();

            await ZuvviiAPIMockData.CreateUsers(application, true);
            var url = "/api/v1/users/08da58cf-bbb5-4e17-8bcd-4e2cd8b41940";

            var client = application.CreateClient();

            var result = await client.GetAsync(url);
            //var user = await client.GetFromJsonAsync<User>("/api/v1/users/08da58cf-bbb5-4e17-8bcd-4e2cd8b41940");

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task POST_Login_Failure()
        {

            await using var application = new ZuvviiApiApplication();

            var user = new DtoLogin { Email = "edson1@gmail.com", Password = "fdwfwef" };

            var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/api/v1/users/login", user);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public async Task POST_Login_Failure2()
        {
            var user = new DtoLogin { Email = "edson1@gmail.com", Password = "fdwfwef" };
            var response = await _client.GetAsync("/api/v1/users/login");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task POST_Login_Sucess()
        {
            await using var application = new ZuvviiApiApplication();

            var user = new DtoLogin { Email = "edson1@gmail.com", Password = "abcdefg1" };

            var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/api/v1/users/login", user);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }


    }
}
