using System;
using Amazon;
using Amazon.Runtime;
using AwsSecretProvider;
using Microsoft.Extensions.Configuration;

namespace AwsSecretProviderRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var credentials = new BasicAWSCredentials("key", "secret");
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddAwsSecretsManager(credentials, RegionEndpoint.EUWest1);

            var config = configBuilder.Build();
            var configSection = config.GetSection("test");

            Console.WriteLine($"{configSection.Key}:{configSection.Value}");
        }
    }
}
