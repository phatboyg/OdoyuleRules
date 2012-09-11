// Copyright 2011 Chris Patterson
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace OdoyuleRules.Configuration.Designer
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using OdoyuleRules.Models.SemanticModel;


    public class ConditionExpressionParserImpl :
        ConditionExpressionParser
    {
        public IEnumerable<RuleCondition> Parse<TFact>(Expression<Func<TFact, bool>> expression)
            where TFact : class
        {
            var visitor = new ConditionExpressionVisitor<TFact>();
            visitor.Visit(expression);

            return visitor.Conditions;
        }
    }
}