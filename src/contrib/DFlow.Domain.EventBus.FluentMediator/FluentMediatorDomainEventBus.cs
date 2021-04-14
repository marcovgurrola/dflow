﻿// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Threading;
using System.Threading.Tasks;
using DFlow.Domain.Events;
using FluentMediator;

namespace DFlow.Domain.EventBus.FluentMediator
{
    public class FluentMediatorDomainEventBus:IDomainEventBus, IDomainEventBusAsync
    {
        private readonly IMediator _mediator;
        
        public FluentMediatorDomainEventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Publish<TEvent>(TEvent request)
        {
            _mediator.Publish(request);
        }

        public TResult Send<TResult,TRequest>(TRequest request)
        {
            return _mediator.Send<TResult>(request);
        }

        public Task PublishAsync<TEvent>(TEvent request, CancellationToken cancellationToken)
        {
            return _mediator.PublishAsync(request,cancellationToken);
        }

        public Task<TResult> SendAsync<TResult, TRequest>(TRequest request, CancellationToken cancellationToken)
        {
            return _mediator.SendAsync<TResult>(request,cancellationToken);
        }
    }
}