using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QnA_Chatbot.WebClient.Models
{
    public class QnAResponse
    {
        [JsonProperty(PropertyName = "answers")]
        public List<QnAAnswer> Answers { get; set; }
    }

    public class QnAAnswer
    {
        [JsonProperty(PropertyName = "questions")]
        public List<string> Questions { get; set; }

        [JsonProperty(PropertyName = "answer")]
        public string Answer { get; set; }

        [JsonProperty(PropertyName = "score")]
        public double Score { get; set; }
    }
}
