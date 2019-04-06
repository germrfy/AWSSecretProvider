using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace AwsSecretProvider
{
    internal interface IAwsSecretManagerService
    {
        Task<IList<SecretListEntry>> ListAllAsync();
        Task<IDictionary<string, string>> GetSecretsAsync(IList<SecretListEntry> secretsToRetrieve);
    }
    internal class AwsSecretManagerService : IAwsSecretManagerService
    {
        private readonly IAmazonSecretsManager _secretsManager;

        public AwsSecretManagerService(IAmazonSecretsManager secretsManager)
        {
            _secretsManager = secretsManager;
        }

        public async Task<IList<SecretListEntry>> ListAllAsync()
        {
            var listSecretsRequest = new ListSecretsRequest 
            { 
                MaxResults = 100
            };
            var listOfSecrets = new List<SecretListEntry>();
            do
            {
                var result = await _secretsManager.ListSecretsAsync(listSecretsRequest);
                if(result?.SecretList.Any() ?? false)
                {
                    listOfSecrets.AddRange(result.SecretList);
                }
                listSecretsRequest.NextToken = result.NextToken;
            }
            while(listSecretsRequest.NextToken != null);
            return listOfSecrets;
        }

        public async Task<IDictionary<string, string>> GetSecretsAsync(IList<SecretListEntry> secretsToRetrieve)
        {
            var secretsDictionary = new ConcurrentDictionary<string, string>();
            foreach(var secret in secretsToRetrieve)
            {
                var secretValueRequest = new GetSecretValueRequest
                {
                    SecretId = secret.ARN
                };
                var secretValue = await _secretsManager.GetSecretValueAsync(secretValueRequest);
                secretsDictionary[secretValue.Name] = secretValue.SecretString;
            }
            return secretsDictionary;
        }
    }
}