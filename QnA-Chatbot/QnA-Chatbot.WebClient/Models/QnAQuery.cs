using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QnA_Chatbot.WebClient.Models
{
    public class QnAQuery
    {
        [JsonProperty(PropertyName = "questions")]
        public string Question { get; set; }

        [JsonProperty(PropertyName = "answer")]
        public string Answer { get; set; }
    }
}
