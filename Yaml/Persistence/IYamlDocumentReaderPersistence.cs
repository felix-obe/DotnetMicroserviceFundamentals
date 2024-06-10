namespace DotnetMicroServiceFundamentals.Yaml.Persistence;

public interface IYamlDocumentReaderPersistence 
{
    T Read<T>(string filePath) where T : struct;
}