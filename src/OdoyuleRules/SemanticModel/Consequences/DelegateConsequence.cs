// Copyright 2011-2012 Chris Patterson
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
namespace OdoyuleRules.SemanticModel
{
    using System;


    public class DelegateConsequence<T> :
        RuleConsequence<T>
        where T : class
    {
        readonly Action<Session, T> _callback;

        public DelegateConsequence(Action<Session, T> callback)
        {
            _callback = callback;
        }

        public Action<Session, T> Callback
        {
            get { return _callback; }
        }

        public bool Accept(SemanticVisitor visitor)
        {
            return visitor.Visit(this, x => true);
        }
    }
}