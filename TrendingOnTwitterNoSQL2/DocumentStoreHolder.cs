using System;
using Raven.Client;
using Raven.Client.Document;

namespace TrendingOnTwitterNoSQL2
{
    class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> LazyStore =
            new Lazy<IDocumentStore>(() =>
            {
                var store = new DocumentStore
                {
                    Url = "http://localhost:8080",
                    DefaultDatabase = "CanadianGP"
                };
                return store.Initialize();
            });

        public static IDocumentStore Store =>
             LazyStore.Value;
    }
}
