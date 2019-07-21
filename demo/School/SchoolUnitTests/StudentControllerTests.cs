using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using School;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SchoolUnitTests
{
    public class StudentControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly string _apiRoute = @"/api/Students";

        public StudentControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task can_add_student()
        {
            using (var client = _factory.CreateClient())
            {
                // Arrange
                var dtoStudent = new School.DTO.Student()
                {
                    Id = 1,
                    FirstName = "Xavier",
                    LastName = "John",
                    ZipCode = "98052"
                };

                var studentJson = JsonConvert.SerializeObject(dtoStudent);
                var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync(_apiRoute, content);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                response.Headers.Location.AbsolutePath.Should().Be($"{_apiRoute}/{dtoStudent.Id}");
                var checkStatusAPI = response.Headers.Location.AbsolutePath;
                response = await client.GetAsync(checkStatusAPI);
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                response = await client.GetAsync(checkStatusAPI);
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                studentJson = await response.Content.ReadAsStringAsync();
                var studentReturned = JsonConvert.DeserializeObject<School.DTO.Student>(studentJson);
                studentReturned.Should().Be(dtoStudent);

            }
        }
    }
}
