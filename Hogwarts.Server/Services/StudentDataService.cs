using Hogwarts.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hogwarts.Server.Services
{
    public class StudentDataService
    {
        private readonly HttpClient _httpClient;

        public StudentDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<StudentDto>>
                (await _httpClient.GetStreamAsync($"api/students"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<StudentDto> GetStudentById(int studentId)
        {
            return await JsonSerializer.DeserializeAsync<StudentDto>
                (await _httpClient.GetStreamAsync($"api/students/{studentId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
