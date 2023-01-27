# TrimQnaMaker

Basic Blazor Server project to list pairs of questions and answer for a Knowledge base in QnA Maker and see the pairs that have some questions above a given number, and for each answer list the questions and be able to delete them.

When migrating from [QnA Maker](https://www.qnamaker.ai/) to the new [Custom Question Answering](https://language.cognitive.azure.com/questionAnswering/projects) service you may encounter a problem, which can be that the knowledge base is empty. This may happen for several reasons, one of the reasons is that the amount of questions in a pair is over the limit, the hard limit is 1000 but is recommended to only have up to 300. You can see [here](https://learn.microsoft.com/en-us/azure/cognitive-services/QnAMaker/limits#knowledge-base-content-limits) the limitations of the new service and from [QnA Maker](https://learn.microsoft.com/en-us/azure/cognitive-services/language-service/question-answering/concepts/limits#knowledge-base-content-limits).

To use this tool for your knowledge base you will need: 

| Name | Description |
|--|---:|
|Endpoint|Supported Cognitive Services endpoint.
|Ocp-Apim-Subscription-Key	 |Subscription Id for the service
|Knowledge Base Id| Id of the Knowledge Base to use
|Amount|Number of questions you want to filter by (Pairs with more that x questions)