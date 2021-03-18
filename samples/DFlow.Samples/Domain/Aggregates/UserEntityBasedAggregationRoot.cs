﻿// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;
using DFlow.Samples.BusinessObjects.Domain.BusinessObjects;
using DFlow.Samples.Domain.Aggregates.Events;
using DFlow.Samples.Domain.BusinessObjects;

namespace DFlow.Samples.Domain.Aggregates
{
    public sealed class UserEntityBasedAggregationRoot : ObjectBasedAggregationRoot<User>
    {

        private UserEntityBasedAggregationRoot(User user)
        {
            if (user.ValidationResults.IsValid)
            {
                Apply(user);

                if (user.IsNew())
                {
                    Raise(UserAddedEvent.For(user));
                }
            }

            ValidationResults = user.ValidationResults;
        }

        #region Aggregation contruction
       
        public static UserEntityBasedAggregationRoot CreateFrom(Name name, Email commercialEmail)
        {
            var user = User.From(EntityId.GetNext(), name, commercialEmail, Version.New());
            return new UserEntityBasedAggregationRoot(user);
        }

        #endregion
    }
}