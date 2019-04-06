using System;
using Amazon;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace AwsSecretProvider
{
    public static class AwsSecretManagerConfigurationExtensions
    {
        public static IConfigurationBuilder AddAwsSecretsManager(this IConfigurationBuilder configurationBuilder,
            AWSCredentials credentials, RegionEndpoint region)
        {
            var awsSecretManagerSource = new AwsSecretManagerConfigurationSource(credentials, 
                new AwsSecretManagerConfigurationProperties(region));

            configurationBuilder.Add(awsSecretManagerSource);
            return configurationBuilder;
        }
    }
}