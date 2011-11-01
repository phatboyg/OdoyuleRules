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
namespace OdoyuleRules.Designer
{
    using System;
    using System.Linq.Expressions;

    public class BindingImpl<T> :
        Binding<T>
        where T : class
    {
        readonly RuleDesigner _designer;

        public BindingImpl(RuleDesigner designer)
        {
            _designer = designer;
        }

        public Binding<T> When(Expression<Func<T, bool>> expression)
        {
            return this;
        }

        public Binding<T> Then(Action<ThenConfigurator<T>> configureCallback)
        {
            var thenConfigurator = new ThenConfiguratorImpl<T>(this);

            configureCallback(thenConfigurator);

            return this;
        }
    }
}