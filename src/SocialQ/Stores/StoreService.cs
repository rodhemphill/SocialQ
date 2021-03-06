using System;
using System.Collections.Generic;
using DynamicData;

namespace SocialQ.Stores
{
    public class StoreService : IStoreService
    {
        private readonly SourceCache<StoreDto, Guid> _stores =
            new SourceCache<StoreDto, Guid>(x => x.Id);
        private readonly SourceList<string> _metadata = new SourceList<string>();
        private readonly IStoreApiClient _apiClient;

        public StoreService(IStoreApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IObservable<IEnumerable<StoreDto>> GetStores(bool forceUpdate = true) =>
            _apiClient
                .GetStores(forceUpdate)
                .AddOrUpdate(_stores);

        public IObservable<IEnumerable<string>> GetStoreMetadata(bool forceUpdate = true) =>
            _apiClient
                .GetStoreMetadata(forceUpdate)
                .AddRange(_metadata);

        public IObservable<StoreDto> GetStore(Guid id, bool forceUpdate = true) =>
            _apiClient
                .GetStore(id, forceUpdate)
                .AddOrUpdate(_stores);

        public IObservable<IChangeSet<string>> Metadata => _metadata.Connect().RefCount();

        public IObservableCache<StoreDto, Guid> Stores => _stores;
    }
}