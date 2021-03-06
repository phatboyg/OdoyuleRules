﻿// Copyright 2011-2012 Chris Patterson
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
namespace OdoyuleRules
{
    using System;
    using Configuration.Configurators;
    using Configuration.RuntimeConfigurators;


    /// <summary>
    /// Used to create an instance of a rules engine
    /// </summary>
    public static class RulesEngineFactory
    {
        /// <summary>
        /// Configures and creates a new rules engine instance
        /// </summary>
        /// <param name="configureCallback">The callback to perform the configuration</param>
        /// <returns>The new rules engine instance</returns>
        public static RulesEngine New(Action<EngineConfigurator> configureCallback)
        {
            if (configureCallback == null)
                throw new ArgumentNullException("configureCallback");

            var configurator = new OdoyuleEngineConfigurator();

            configureCallback(configurator);

            ConfigurationResult result = configurator.Validate();

            try
            {
                RulesEngine engine = configurator.Create();

                return engine;
            }
            catch (Exception ex)
            {
                throw new RulesEngineConfigurationException(result, ex);
            }
        }
    }
}