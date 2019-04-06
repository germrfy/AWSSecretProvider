using Amazon.Runtime;
using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;

namespace AwsSecretProvider
{
    internal class AwsSecretManagerConfigurationSource : IConfigurationSource
    {
        private readonly AWSCredentials _credentials;
        private readonly AwsSecretManagerConfigurationProperties _properties;

        public AwsSecretManagerConfigurationSource(AWSCredentials credentials, 
            AwsSecretManagerConfigurationProperties properties)
        {
            _credentials = credentials;
            _properties = properties;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var secretManagerService = new AwsSecretManagerService(
                new AmazonSecretsManagerClient(_credentials, _properties.Region));
            return new AwsSecretManagerConfigurationProvider(secretManagerService);
        }
    }
}