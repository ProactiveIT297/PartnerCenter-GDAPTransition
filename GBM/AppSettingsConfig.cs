﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Reflection;

namespace PartnerLed
{
    /// <summary>
    /// Description of the configuration of an AzureAD public client application (desktop/mobile application). This should
    /// match the application registration done in the Azure portal
    /// </summary>
    public class AppSettingsConfiguration
    {
        /// <summary>
        /// Authentication options
        /// </summary>
        public PublicClientApplicationOptions PublicClientApplicationOptions { get; set; }

        /// <summary>
        /// Base URL for Microsoft Graph (it varies depending on whether the application is ran
        /// in Microsoft Azure public clouds or national / sovereign clouds
        /// </summary>
        public string MicrosoftGraphBaseEndpoint { get; set; }

        public string GdapEndPoint { get; set; }

        /// <summary>
        /// Reads the configuration from a json file
        /// </summary>
        /// <param name="path">Path to the configuration json file</param>
        /// <returns>SampleConfiguration as read from the json file</returns>
        public static AppSettingsConfiguration ReadFromJsonFile(string path)
        {
            // .NET configuration
            IConfigurationRoot Configuration;

            var builder = new ConfigurationBuilder()
             .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
             .AddJsonFile($"Configuration/{path}");

            Configuration = builder.Build();

            // Read the auth and graph endpoint config
            AppSettingsConfiguration config = new AppSettingsConfiguration()
            {
                PublicClientApplicationOptions = new PublicClientApplicationOptions()
            };
            Configuration.Bind("Authentication", config.PublicClientApplicationOptions);
            config.MicrosoftGraphBaseEndpoint = Configuration.GetValue<string>("WebAPI:MicrosoftGraphBaseEndpoint");
            config.GdapEndPoint = Configuration.GetValue<string>("WebAPI:GdapEndPoint");
            return config;
        }
    }
}
