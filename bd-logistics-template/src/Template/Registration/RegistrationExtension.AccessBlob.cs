using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Template.API.AccessBlob;

namespace Template.API.Registration
{
    /// <summary>
    /// Creating a RegistrationExtension method for blob stoarge
    /// </summary>
    public static partial class RegistrationExtension
    {
        /// <summary>
        /// extension method BlobFiles to fetch schema files from the Blob
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static IServiceCollection BlobFiles(this IServiceCollection services,
            IConfiguration configuration, ILogger logger)
        {
            Dictionary<string, string> Files = GetBlobFiles(configuration, logger).GetAwaiter().GetResult();
            AccessBlobFiles.dictBlobFiles = Files;
            return services;
        }
        private static async Task<Dictionary<string, string>> GetBlobFiles(IConfiguration config, ILogger logger)
        {
            logger.LogInformation("Started reading JSON schema files from Blob Storage");
            var FileList = SettingBySection(config, "Blob:Files");
            CloudBlobContainer cloudBlobContainer = null;
            string storageConnectionString = config.GetSection("Blob:ConnectionString").Value;

            try
            {
                if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
                {
                    try
                    {
                        // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                        // Use a container called 'testaditi'
                        cloudBlobContainer = cloudBlobClient.GetContainerReference(config.GetSection("Blob:ContainerName").Value);

                        //Set the permissions so the blobs are public. 
                        BlobContainerPermissions permissions = new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        };
                        await cloudBlobContainer.SetPermissionsAsync(permissions);

                        foreach (var item in FileList.Keys.ToList())
                        {
                            string text = string.Empty;
                            string jsonFile = $"{item}{config.GetSection("Blob:ExtensionType").Value}";

                            var blob = await cloudBlobContainer.GetBlockBlobReference(jsonFile).ExistsAsync();
                            if (blob)
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    await cloudBlobContainer.GetBlockBlobReference(jsonFile).DownloadToStreamAsync(memoryStream);
                                    text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                                }
                                if (FileList.ContainsKey(item))
                                {
                                    FileList[item] = text;
                                    logger.LogInformation("Reading of JSON files from Blob Storage completed!");
                                }
                                logger.LogInformation(item + " file exist in blob");
                            }
                            else
                            {
                                logger.LogError(item + " file does not exist in blob");
                            }
                        }
                    }
                    catch (StorageException ex)
                    {
                        logger.LogError(ex.Message);
                        throw new Exception("Problem with fetching schema details from Blob storage.");
                    }
                }
                else
                {
                    logger.LogError("Blob storage connection cannot be established.");
                    throw new Exception("Blob storage connection cannot be established.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Blob storage connection cannot be established, other possible error: ", ex.Message);
                throw new Exception("Blob storage connection cannot be established, other possible error: ", ex);
            }
            return new Dictionary<string, string>(FileList);
        }
        private static Dictionary<string, string> SettingBySection(IConfiguration configuration, string name)
        {
            return configuration.GetSection(name).GetChildren()
                .Select(item => new KeyValuePair<string, string>(item.Value, ""))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
