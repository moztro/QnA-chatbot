using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QnA_Chatbot.WebClient.Models;
using RestSharp;

namespace QnA_Chatbot.WebClient.Controllers
{
    public class ChatController : Controller
    {
        private string _ocpApimSubscriptionKey;
        private string _knowledgeBase;
        private string _baseURL;

        public ChatController(IOptions<CustomSettings> customSettings)
        {
            _ocpApimSubscriptionKey = customSettings.Value.OcpApimSubscriptionKey;
            _knowledgeBase = customSettings.Value.KnowledgeBase;
            _baseURL = customSettings.Value.BaseURL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            QnAQuery model = new QnAQuery();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(QnAQuery query)
        {
            QnAQuery result = Ask(query.Question);
            return View(result);
        }

        [HttpGet]
        public QnAQuery Ask(string question)
        {
            QnAQuery query = new QnAQuery();
            query.Question = question;

            string requestUri = string.Format("{0}{1}{2}{3}",
                "qnamaker/v2.0/",
                "knowledgebases/",
                _knowledgeBase,
                "/generateAnswer");

            RestRequest request = new RestRequest(requestUri, Method.POST);
            request.AddJsonBody(query);
            request.AddHeader("Ocp-Apim-Subscription-Key", _ocpApimSubscriptionKey);

            RestClient client = new RestClient(_baseURL);
            IRestResponse<QnAResponse> response = client.Execute<QnAResponse>(request);

            if (response.IsSuccessful)
            {
                query.Answer = response.Data.Answers[0].Answer;
            }

            return query;
        }
    }
}