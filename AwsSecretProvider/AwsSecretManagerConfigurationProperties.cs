using System;
using Amazon;

namespace AwsSecretProvider
{
    internal class AwsSecretManagerConfigurationProperties
    {
        public AwsSecretManagerConfigurationProperties(RegionEndpoint region)
        {
            Region = region ?? throw new ArgumentNullException(nameof(region));
        }

        public RegionEndpoint Region { get; set; }
    }
}