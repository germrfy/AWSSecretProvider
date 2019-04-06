using System;
using System.Threading.Tasks;
using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;

namespace AwsSecretProvider
{
    internal class AwsSecretManagerConfigurationProvider : ConfigurationProvider
    {
        private readonly IAwsSecretManagerService _secretManagerService;

        public AwsSecretManagerConfigurationProvider(IAwsSecretManagerService secretManagerService)
        {
            _secretManagerService = secretManagerService;
        }

        public override void Load()
        {
            LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private async Task LoadAsync()
        {
            var listedSecrets = await _secretManagerService.ListAllAsync();
            var secretValues = await _secretManagerService.GetSecretsAsync(listedSecrets);
            foreach(var secretValue  in secretValues)
            {
                Set(secretValue.Key, secretValue.Key);
            }
        }
    }
}
