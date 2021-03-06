using System;
using System.Reactive;
using Akavache;

namespace SocialQ.Queue
{
    public interface IQueueApiClient
    {
        IBlobCache BlobCache { get; }

        IObservable<QueuedStoreDto> Enqueue(EnqueueRequest dto);

        IObservable<QueuedStoreDto> GetQueue(Guid userId, bool forceUpdate = false);
    }
}