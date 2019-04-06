# AwsSecretProvider

AwsSecretProvider is an implementation of a .NET Core IConfiguration provider that retrieves secrets from AWS Secrets Manager.
At the moment there is only support for simple string key values.

Note: There is a more feature rich implementation of this [here](https://github.com/Kralizek/AWSSecretsManagerConfigurationExtensions)

## TODO

* Tests!
* Support JSON secret values
* Error handling

## Usage

``` csharp
var credentials = new BasicAWSCredentials("key", "secret");
var configBuilder = new ConfigurationBuilder();
configBuilder.AddAwsSecretsManager(credentials, RegionEndpoint.EUWest1);
```