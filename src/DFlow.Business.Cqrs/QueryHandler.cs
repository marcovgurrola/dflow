﻿// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Threading;
using System.Threading.Tasks;
using DFlow.Business.Cqrs.QueryHandlers;

namespace DFlow.Business.Cqrs
{
    public abstract class QueryHandler<TFilter, TResult> : IQueryHandler<TFilter, TResult>
    {
        public async Task<TResult> Execute(TFilter filter, CancellationToken cancellationToken = default)
        {
            return await ExecuteQuery(filter, cancellationToken).ConfigureAwait(false);
        }

        protected abstract Task<TResult> ExecuteQuery(TFilter filter, CancellationToken cancellationToken);
    }
}