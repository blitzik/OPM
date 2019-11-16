using System;
using System.Collections.Generic;
using System.IO;
using Measurement.Services.RavenDB.Indexes;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Session;

namespace Measurement.Services.RavenDB
{
    public class RavenDatabase : IDisposable
    {
        private IDocumentStore _documentStore;
        private Dictionary<string, IDocumentSession> _namedSessions;
        
        
        public RavenDatabase(string databaseName, string serverUrl = "http://127.0.0.1:8080")
        {
            _documentStore = new DocumentStore()
            {
                Urls = new[]
                {
                    serverUrl
                },
                Database = databaseName,
                Conventions = {}
            };
            
            _documentStore.Initialize();
            _namedSessions = new Dictionary<string, IDocumentSession>();
            RegisterIndexes();
        }


        public IDocumentSession GetSession(string name = "opm")
        {
            if (!_namedSessions.ContainsKey(name)) {
                _namedSessions.Add(name, _documentStore.OpenSession());
            }
            
            return _namedSessions[name];
        }


        private void RegisterIndexes()
        {
            new Items_ByOrderName().ExecuteAsync(_documentStore);
        }
        

        public void Dispose()
        {
            foreach (KeyValuePair<string, IDocumentSession> pair in _namedSessions)
            {
                pair.Value.Dispose();
            }
            
            _documentStore.Dispose();
        }
    }
}