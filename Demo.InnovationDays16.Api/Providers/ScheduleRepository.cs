using Demo.Innovationdays16.Api.Models;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Innovationdays16.Api.DataAccess
{
    public static class ScheduleRepository
    {
        private static readonly string DatabaseId = ConfigurationManager.AppSettings["database"];
        private static readonly string CollectionId = ConfigurationManager.AppSettings["collection"];

        public static async Task<SessionSelections> GetScheduleAsync(string userId)
        {
            try
            {
                using (DocumentClient client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"]))
                {

                    var query = client.CreateDocumentQuery<SessionSelections>(
                                            UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                                            new FeedOptions { MaxItemCount = 1 })
                                            .Where(s => s.Userid == userId)
                                            .AsDocumentQuery();

                    var result = await query.ExecuteNextAsync<SessionSelections>();

                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                // clearly unhelpful, but will do here.
                return new SessionSelections();
            }
        }

        public static async Task<bool> SaveScheduleAsync(SessionSelections sessions)
        {
            var result = false;

            try
            {
                using (DocumentClient client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"]))
                {
                    if (string.IsNullOrWhiteSpace(sessions.id))
                    {
                        await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), sessions);
                    }
                    else
                    {
                        await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, sessions.id), sessions);
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
            }

            return result;
        }
    }
}