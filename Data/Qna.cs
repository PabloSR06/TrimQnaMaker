using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrimQnaMaker.Data
{
    public partial class KeysModel
    {
        //Supported Cognitive Services endpoint
        public string endpoint { get; set; }
        //Ocp-Apim-Subscription-Key
        public string subscriptionId { get; set; }
        //Knowledgebase id 
        public string kbid { get; set; }
        //Amount of questions to filter
        public int amount { get; set; }
    }
    public partial class QnaDocumentMin
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("questions")]
        public List<string> Questions { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }

        public bool hasChanged { get; set; }
        public List<string> QuestionToDelete { get; set; }

    }

    public partial class QnaMin
    {
        [JsonProperty("qnaDocuments")]
        public List<QnaDocumentMin> QnaDocuments { get; set; }
    }

    public partial class DeleteQuestions
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("questions")]
        public Metadata Questions { get; set; }
    }
    public partial class Metadata
    {
        [JsonProperty("delete")]
        public List<string> delete { get; set; }


    }

    public partial class toUpdate
    {
        [JsonProperty("update")]
        public valueToUpdate update { get; set; }
    }

    public partial class valueToUpdate
    {
        [JsonProperty("name")]
        public String name { get; set; }
        
        [JsonProperty("qnaList")]
        public List<DeleteQuestions> qnaList { get; set; }
        
    }

    public partial class JobResult
    {
        [JsonProperty("operationState")]
        public string OperationState { get; set; }

        [JsonProperty("createdTimestamp")]
        public DateTimeOffset CreatedTimestamp { get; set; }

        [JsonProperty("lastActionTimestamp")]
        public DateTimeOffset LastActionTimestamp { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("operationId")]
        public Guid OperationId { get; set; }
    }
}
