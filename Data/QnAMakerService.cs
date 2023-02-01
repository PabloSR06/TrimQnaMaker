using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace TrimQnaMaker.Data
{
    internal class QnAMakerService
    {
        public Task<List<QnaDocumentMin>> GetBadQna(KeysModel keys)
        {
            HttpClient client = GetHttpClient(keys.endpoint, keys.subscriptionId);
            HttpResponseMessage response = client.GetAsync($"{keys.kbid}/Test/qna").Result;

            QnaMin qna = new QnaMin();
            List<QnaDocumentMin> badPairs = new List<QnaDocumentMin>();

            if (response.IsSuccessStatusCode)
            {
                qna = JsonConvert.DeserializeObject<QnaMin>(response.Content.ReadAsStringAsync().Result);


                if (qna != null)
                {
                    foreach (var pair in qna.QnaDocuments)
                    {
                        if (pair.Questions.Count >= keys.amount)
                        {
                            badPairs.Add(pair);
                        }
                    }
                }
                
            }
            
            return Task.FromResult(badPairs);
        }

        public async Task<JobResult> UpgradeKB(List<QnaDocumentMin> qnas, KeysModel keys)
        {
            HttpClient client = GetHttpClient(keys.endpoint, keys.subscriptionId);

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{keys.kbid}");

            List<DeleteQuestions> delete = new List<DeleteQuestions>();

            foreach (var x in qnas) {

                if (x.hasChanged)
                {
                    var toDelete = new DeleteQuestions();

                    toDelete.Id = x.Id;
                    toDelete.Questions = new Metadata
                    {
                        delete = x.QuestionToDelete,
                    };
                    delete.Add(toDelete);
                }
            }
            toUpdate update = new toUpdate();
            update.update = new valueToUpdate();    
            update.update.qnaList = delete;
            var json = JsonConvert.SerializeObject(update);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            var responseJson = new JobResult();

            if (response.IsSuccessStatusCode)
            {
                responseJson = JsonConvert.DeserializeObject<JobResult>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                throw new Exception(string.Format("Update QNA failed '{0}'", response.ReasonPhrase));
            }
               


            return responseJson;
        }

        public static HttpClient GetHttpClient(string endpoint, string subscription)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri($"{endpoint}/qnamaker/v4.0/knowledgebases/"),
                Timeout = new TimeSpan(0, 2, 0)    // Standard two minute timeout on web service calls.
            };
            HttpRequestHeaders headers = client.DefaultRequestHeaders;


            headers.Add("OData-MaxVersion", "4.0");
            headers.Add("Ocp-Apim-Subscription-Key", $"{subscription}");
            headers.Add("OData-Version", "4.0");
            headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }

   

    
}
