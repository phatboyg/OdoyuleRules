﻿// Copyright 2011 Chris Patterson
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
namespace OdoyuleRules.Visualization
{
    using System;
    using System.Text;
    using Models.RuntimeModel;

    public class TextRuntimeModelVisitor :
        RuntimeModelVisitor
    {
        const int PaddingWidth = 2;
        int _depth;
        string _padding;
        StringBuilder _sb;

        public TextRuntimeModelVisitor()
        {
            _sb = new StringBuilder();

            _depth = 0;
            _padding = "";
        }

        public virtual bool Visit(RulesEngine rulesEngine, Func<RuntimeModelVisitor, bool> next)
        {
            Append("Rules Engine");

            return Indent(next);
        }

        public virtual bool Visit<T>(JoinNode<T> node, Func<RuntimeModelVisitor, bool> next) where T : class
        {
            Append("JoinNode[{0}]", Tokens<T>());

            return Indent(next);
        }

        public bool Visit<T, TOutput>(LeftJoinNode<T, TOutput> node, Func<RuntimeModelVisitor, bool> next)
            where T : class
        {
            Append("LeftJoinNode[{0}] => {1}", Tokens<T>(), Tokens<TOutput>());

            return Indent(next);
        }

        public virtual bool Visit<T>(AlphaNode<T> node, Func<RuntimeModelVisitor, bool> next) where T : class
        {
            Append("AlphaNode[{0}]", Tokens<T>());

            return Indent(next);
        }

        public virtual bool Visit<TInput, TOutput>(ConvertNode<TInput, TOutput> node,
                                                   Func<RuntimeModelVisitor, bool> next) where TInput : class, TOutput
            where TOutput : class
        {
            Append("ConvertNode[{0}] => {1}", Tokens<TInput>(), Tokens<TOutput>());

            return Indent(next);
        }

        public virtual bool Visit<T>(DelegateProductionNode<T> node, Func<RuntimeModelVisitor, bool> next)
            where T : class
        {
            Append("DelegateProductionNode[{0}]", Tokens<T>());

            return Indent(next);
        }

        public virtual bool Visit<T, TProperty>(PropertyNode<T, TProperty> node, Func<RuntimeModelVisitor, bool> next)
            where T : class
        {
            Append("PropertyNode[{0}].{1} ({2})", Tokens<T>(), node.PropertyInfo.Name, typeof (TProperty).Name);

            return Indent(next);
        }

        public virtual bool Visit<T>(ConstantNode<T> node, Func<RuntimeModelVisitor, bool> next) where T : class
        {
            Append("v[{0}]", Tokens<T>());

            return Indent(next);
        }

        public virtual bool Visit<T>(ConditionNode<T> node, Func<RuntimeModelVisitor, bool> next) where T : class
        {
            Append("ConditionNode[{0}]", Tokens<T>());

            return Indent(next);
        }

        public virtual bool Visit<T, TProperty>(EqualNode<T, TProperty> node,
                                                Func<RuntimeModelVisitor, bool> next) where T : class
        {
            Append("EqualNode[{0}] ({1})", Tokens<T>(), typeof (TProperty).Name);

            return Indent(next);
        }

        public bool Visit<T, TProperty>(ValueNode<T, TProperty> node, Func<RuntimeModelVisitor, bool> next)
            where T : class
        {
            Append("ValueNode[{0}] == {1}", Tokens<T>(), node.Value);

            return Indent(next);
        }

        public bool Visit<T, TProperty>(CompareNode<T, TProperty> node, Func<RuntimeModelVisitor, bool> next) 
            where T : class
        {
            Append("CompareNode[{0},{1}] {2} {3}", Tokens<T>(), typeof (TProperty).Name, node.Comparator.ToString(), node.Value.ToString());

            return Indent(next);
        }

        void Append(string format, params object[] args)
        {
            Append(string.Format(format, args));
        }

        void Append(string text)
        {
            _sb.Append(_padding).AppendLine(text);
        }

        string Tokens<T>()
        {
            return Tokens(typeof (T));
        }

        string Tokens(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Token<,>))
            {
                Type[] arguments = type.GetGenericArguments();

                return string.Join(",", Tokens(arguments[0]), arguments[1].Name);
            }

            return type.Name;
        }

        bool Indent(Func<RuntimeModelVisitor, bool> next)
        {
            _depth++;
            string previous = _padding;
            _padding = new string(' ', _depth*PaddingWidth);

            bool result = next(this);

            _depth--;
            _padding = previous;

            return result;
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }
}