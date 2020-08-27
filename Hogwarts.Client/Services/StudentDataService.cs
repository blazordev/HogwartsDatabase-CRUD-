using Hogwarts.Client.Helpers;
using Hogwarts.Data.Models;
using Hogwarts.Data.ResourceParameters;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hogwarts.Client.Services
{
    public class StudentDataService
    {

        private readonly HttpClient _httpClient;

        public StudentDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagingResponse<StudentDto>> GetAllStudentsAsync(StudentsResourceParameters studentParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["SearchQuery"] = studentParameters.SearchQuery ?? "",
                ["HouseId"] = studentParameters.HouseId.ToString(),
                ["PageNumber"] = studentParameters.PageNumber.ToString()
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"{ _httpClient.BaseAddress}api/students", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content);
            }
            var pagingResponse = new PagingResponse<StudentDto>
            {
                Items = JsonSerializer.Deserialize<List<StudentDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<PaginationMetadata>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }
        //public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        //{
        //    return await JsonSerializer.DeserializeAsync<IEnumerable<StudentDto>>
        //        (await _httpClient.GetStreamAsync($"api/students"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //}

        public async Task<StudentDto> GetStudentByIdAsync(int studentId)
        {
            return await JsonSerializer.DeserializeAsync<StudentDto>
                (await _httpClient.GetStreamAsync($"api/students/{studentId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<StudentDto> AddStudentAsync(StudentDto student)
        {
            var studentJson =
                new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/students", studentJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<StudentDto>(await response.Content.ReadAsStreamAsync());
            }
            return null;
        }

        public async Task DeleteStudentCollection(string studentIds)
        {
            await _httpClient.DeleteAsync($"api/studentCollections/({studentIds})");
        }

        public async Task<string> UpdateStudentCollection(IEnumerable<StudentDto> studentCollection)
        {
            var studentCollectionJson =
                new StringContent(JsonSerializer.Serialize(studentCollection), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/studentCollections", studentCollectionJson);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }
        public async Task<byte[]> Download()
        {
            var response = await _httpClient.GetAsync("api/files");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}

