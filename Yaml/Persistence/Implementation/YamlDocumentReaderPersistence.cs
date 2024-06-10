using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DotnetMicroServiceFundamentals.Yaml.Persistence.Implementation;

public class YamlDocumentReaderPersistence : IYamlDocumentReaderPersistence
{
    public T Read<T>(string filePath) where T : struct
    {
        using (var reader = new StreamReader(filePath))
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var result = deserializer.Deserialize<T>(reader);
            return result;
        }
    }
}
