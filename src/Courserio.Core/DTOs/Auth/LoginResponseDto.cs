using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Courserio.Core.DTOs.Auth
{
    public class LoginResponseDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }

    }
}
