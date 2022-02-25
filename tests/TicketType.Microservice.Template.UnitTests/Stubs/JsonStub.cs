using System.Diagnostics.CodeAnalysis;

namespace TicketType.Microservice.Template.UnitTests.Stubs
{
    [ExcludeFromCodeCoverage]
    public static class JsonStub
    {
        public static string GetGoodJson(string eventType, string entityType)
        {
            return "{" +
                "\"Type\":\"Notification\",\"MessageId\":\"720c5d59-981b-5d4d-a5d5-07d0424134bc\"," +
                "\"TopicArn\":\"arn:aws:sns:us-east-1:663374859601:eng-solution-orchestrator\"," +
                "\"Message\":" + GetMessaegBodyJson(eventType, entityType) + "," +
                "\"Timestamp\":\"2022-02-18T17:32:10.373Z\",\"SignatureVersion\":\"1\",\"Signature\":\"Dd2A8eFGRiLk8AdbjuU9SjAJCqxJ388b04UOPyAl9EZ0ujm+ZgX1rdM8IkD+9fTBJoZ6JgB0UGJWrunA7ltpnb7Sdk2IP3PYWv6Vsz7JuD700R/OR9PT701YlsR9Z6KfeNvvMhEl+vHzUEglJL9VQqGORtvK4MGM5Ee9eB9pAn2Iq/tBIlldu9k6MFj56G4Lro1E6uuN1EO7HCe51jRQiJxqGTEU002QF4oHtdUePT8kYwF6D7WL9wxSrYGZByT18RxidVrPUvvlpJcO0k6q9z4XOxwnRI4Vz0I1BPul49ycAEjC9YC4GZI7z1r/0Yuww7DR9KF/RVoEjUcSHPMX/A==\"," +
                "\"SigningCertURL\":\"https://sns.us-east-1.amazonaws.com/SimpleNotificationService-7ff5318490ec183fbaddaa2a969abfda.pem\"," +
                "\"UnsubscribeURL\":\"https://sns.us-east-1.amazonaws.com/?Action=Unsubscribe&SubscriptionArn=arn:aws:sns:us-east-1:663374859601:eng-solution-orchestrator:0ba84fea-37be-4a8f-93a8-2e1bf82b9cd7\"," +
                "\"MessageAttributes\":{" +
                    "\"workflow_version\":{\"Type\":\"String\",\"Value\":\"4e6165f2-af47-495c-965a-4a1036850c2b\"}," +
                    "\"event_type\":{\"Type\":\"String\",\"Value\":\"task_Started\"}," +
                    "\"job_name\":{\"Type\":\"String\",\"Value\":\"CsvImport\"}," +
                    "\"workflow_name\":{\"Type\":\"String\",\"Value\":\"AO-1105\"}" +
                "}" +
            "}";
        }

        public static string GetMessaegBodyJson(string eventType, string entityType)
        {
            return "\"{" +
               "\\\"Id\\\":\\\"03fa2bb8-50ac-42d1-9d84-04f860462c8c\\\",\\\"JobId\\\":\\\"9ca8c5a9-51b2-471a-9020-cb628c0a5fe9\\\"," +
               "\\\"ParentJobId\\\":null,\\\"WorkflowId\\\":\\\"a149bb87-d845-4aea-b17f-b7c310c3ef6b\\\",\\\"WorkflowBlueprintName\\\":\\\"AO-1105\\\"," +
               "\\\"JobBlueprintName\\\":\\\"CsvImport\\\",\\\"Version\\\":\\\"4e6165f2-af47-495c-965a-4a1036850c2b\\\",\\\"Status\\\":\\\"InProgress\\\"," +
               "\\\"EventType\\\":\\\"" + eventType + "\\\",\\\"TaskType\\\":\\\"TriggerEvent\\\",\\\"Metadata\\\":{" +
                   "\\\"Record\\\":\\\"entity-importer/Tractors-AO-1105-131666917.csv\\\"," +
                   "\\\"Bucket\\\":\\\"eng-entity-import-01pb673zvt4fi4y3\\\"," +
                   "\\\"EntityType\\\":\\\"" + entityType + "\\\"" +
               "}" +
            "}\"";
        }


        public static string GetBadJson()
        {
            return @"{""message""""Is the loneliest number""}";
        }
    }
}