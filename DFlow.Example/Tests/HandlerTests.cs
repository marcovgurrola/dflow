using System;
using DFlow.Bus;
using DFlow.Interfaces;
using DFlow.Store;
using Program.Aggregates;
using Program.Commands;
using Program.Handlers;
using Xunit;

namespace Program.Tests
{
    public class HandlerTests : IDisposable
    {
        private IAppendOnlyStore<Guid> _appendOnly = null;
        private IEventBus _eventBus = null;
        private IEventStore<Guid> _eventStore = null;
        private ISnapshotRepository<Guid> _snapShotRepo = null;
        private AggregateFactory _factory = null;
        
        public HandlerTests()
        {
            _eventBus = new MemoryEventBus();
            _appendOnly = new MemoryAppendOnlyStore(_eventBus);
            _eventStore = new EventStore(_appendOnly);
            _snapShotRepo = new SnapshotRepository();
            _factory = new AggregateFactory(_eventStore, _snapShotRepo);
        }
        
        [Fact]
        public void ShouldCreateProductCatalog()
        {
            var rootId = Guid.NewGuid();
            var handler = new ProductServiceCommandHandler(_eventStore, _factory);
            
            handler.Execute(new CreateProductCatalog(rootId));
            handler.Execute(new CreateProductCommand(rootId, Guid.NewGuid(), "Notebook Lenovo 2 em 1 ideapad C340", "Notebook Lenovo 2 em 1 ideapad C340 i7-8565U 8GB 256GB SSD Win10 14' FHD IPS - 81RL0001BR"));

            var stream = _eventStore.LoadEventStream(rootId);
            var productAggregate = new ProductCatalogAggregate(stream);
            
            Assert.True(stream.Version == 2);
            Assert.True(1 == productAggregate.CountProducts());
        }

        public void Dispose()
        {
            _appendOnly = null;
            _eventBus = null;;
            _eventStore = null;;
            _snapShotRepo = null;;
            _factory = null;;
        }
    }
}