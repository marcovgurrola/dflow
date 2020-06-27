using System;
using System.Collections.Generic;
using Core.Shared;
using DFlow.Interfaces;
using Program.Events;
using Program.Handlers;

namespace Program.Aggregates
{
    public class PurchaseOrderAggreagate
    {
        public bool ConsumptionLocked { get; private set; }
        public List<IEvent> Changes { get; private set; }

        public PurchaseOrderAggreagate(IEnumerable<IEvent> events)
        {
            Changes = new List<IEvent>();
            
            foreach (var @event in events)
            {
                Mutate(@event);
            }
        }

        public void AddProduct(decimal qtd, Guid productId, IProductCatalogCommandHandler productService)
        {
            
        }
        
        void Apply(IEvent @event)
        {
            Changes.Add(@event);
            Mutate(@event);
        }
        
        private void Mutate(IEvent e)
        {
            ((dynamic) this).When((dynamic)e);
        }
    }
}