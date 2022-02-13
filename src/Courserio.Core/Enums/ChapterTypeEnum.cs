using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Courserio.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ChapterTypeEnum
    {
        Video,
        Text,
        Quiz
    }
}
