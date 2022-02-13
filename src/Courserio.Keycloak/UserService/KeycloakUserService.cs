using Courserio.Keycloak.Models;
using Courserio.KeyCloak;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Courserio.Keycloak.UserService
{
    public class KeycloakUserService : IKeycloakUserService
    {
        private readonly HttpClient _httpClient;
        private readonly KeyCloakOptions _options;

        public KeycloakUserService(IOptions<KeyCloakOptions> options)
        {
            _httpClient = new HttpClient();
            _options = options.Value;
        }

        public async Task<KeycloakResponse> LoginAsync(LoginDto loginDto)
        {
            var url = _options.Authority + "/protocol/openid-connect/token";
            var body = new
            {
                grant_type = "password",
                username = loginDto.Username,
                password = loginDto.Password,
                client_id = _options.ClientId,
                client_secret = _options.ClientSecret
            };
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(), "application/json"},
                    {HttpRequestHeader.ContentType.ToString(), "application/x-www-form-urlencoded"},
                    {"X-Version", "1"}
                },
                //Content = new StringContent("data=" + HttpUtility.UrlEncode(JsonSerializer.Serialize(body)), Encoding.UTF8, "application/x-www-form-urlencoded")
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", loginDto.Username),
                    new KeyValuePair<string, string>("password", loginDto.Password),
                    new KeyValuePair<string, string>("client_id", _options.ClientId),
                    new KeyValuePair<string, string>("client_secret", _options.ClientSecret),
                })
            };

            var response = await _httpClient.SendAsync(httpRequestMessage);

            return new KeycloakResponse
            {
                HttpStatusCode = response.StatusCode,
                Content = await response.Content.ReadAsStringAsync()
            };
        }

        public async Task<KeycloakResponse> RegisterAsync(RegisterDto registerDto)
        {
            var url = _options.Host + "/auth/admin/realms/Courserio/users";
            var body = new
            {
                username = registerDto.Username,
                email = registerDto.Email,
                enabled = "true",
                credentials = new[]
                {
                    new
                    {
                        type = "password",
                        value = registerDto.Password
                    }
                }

            };
            var token = await AuthAsync();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(), "application/json"},
                    {HttpRequestHeader.ContentType.ToString(), "application/json"},
                    {"X-Version", "1"},
                    {HttpRequestHeader.Authorization.ToString(), $"Bearer {token}"},
                },
                Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(httpRequestMessage);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                return new KeycloakResponse
                {
                    HttpStatusCode = response.StatusCode,
                    Content = await response.Content.ReadAsStringAsync()
                };
            }
            return new KeycloakResponse
            {
                HttpStatusCode = response.StatusCode,
                Content = await GetUserIdAsync(registerDto.Username)
            };
        }

        public async Task AddRoleToUserAsync(string userId, RoleDto role)
        {
            var url = _options.Host + $"/auth/admin/realms/Courserio/users/{userId}/role-mappings/clients/{_options.Id}"; ;
            var token = await AuthAsync();
            var content = $"[{JsonSerializer.Serialize(role)}]";
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(), "application/json"},
                    {"X-Version", "1"},
                    {HttpRequestHeader.Authorization.ToString(), $"Bearer {token}"},
                },
                Content = new StringContent(content, Encoding.UTF8, "application/json")

            };

            var response = await _httpClient.SendAsync(httpRequestMessage);
        }

        public async Task RemoveRoleFromUserAsync(string userId, RoleDto role)
        {
            var url = _options.Host + $"/auth/admin/realms/Courserio/users/{userId}/role-mappings/clients/{_options.Id}"; ;
            var token = await AuthAsync();
            var content = $"[{JsonSerializer.Serialize(role)}]";
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(), "application/json"},
                    {"X-Version", "1"},
                    {HttpRequestHeader.Authorization.ToString(), $"Bearer {token}"},
                },
                Content = new StringContent(content, Encoding.UTF8, "application/json")

            };

            var response = await _httpClient.SendAsync(httpRequestMessage);
        }

        private async Task<string> GetUserIdAsync(string username)
        {
            var url = _options.Host + $"/auth/admin/realms/Courserio/users?username={username}"; ;
            var token = await AuthAsync();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(), "application/json"},
                    {"X-Version", "1"},
                    {HttpRequestHeader.Authorization.ToString(), $"Bearer {token}"},
                },
            };

            var response = await _httpClient.SendAsync(httpRequestMessage);
            var content = await response.Content.ReadAsStringAsync();
            return JArray.Parse(content)[0]!["id"]!.ToString();
        }
    

    private async Task<string> AuthAsync()
        {
            var url = _options.Authority + "/protocol/openid-connect/token";
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Headers =
                {
                    { HttpRequestHeader.Accept.ToString(), "application/json"
                    },
                    { HttpRequestHeader.ContentType.ToString(), "application/x-www-form-urlencoded"
                    },
                    { "X-Version", "1"
                    }
                },
                Content = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", _options.ClientId),
                    new KeyValuePair<string, string>("client_secret", _options.ClientSecret),
                })
            };
            var response = await _httpClient.SendAsync(httpRequestMessage);
            var content = await response.Content.ReadAsStringAsync();
            return JObject.Parse(content)!["access_token"]!.ToString();
        }
    }
}
