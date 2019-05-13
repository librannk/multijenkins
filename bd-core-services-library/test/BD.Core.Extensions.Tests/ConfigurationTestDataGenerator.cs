
using Xunit;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BD.Core.Extensions.Tests
{
    public class ConfigurationTestDataGenerator : IEnumerable<object[]>
    {
        /// <summary>
        /// Gets the cors policy data.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetCorsPolicyData()
        {
            yield return new object[]
            {
                new Dictionary<string, string>
                {
                    {"SecurityConfiguration:CorsPolicy:AllowWhiteListDomains", "[ \"*\" ]"},
                    {"SecurityConfiguration:CorsPolicy:AllowHeaders", "[ \"*\" ]"},
                    {"SecurityConfiguration:CorsPolicy:AllowMethods", "[ \"*\" ]"}
                }

            };
        }

        /// <summary>
        /// Gets the configuration for security application group.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetConfigurationForSecurityApplicationGroup()
        {
            yield return new object[]
            {
                new Dictionary<string, string>
                {
                    {"SecurityConfiguration:SecurityApplicationGroup", "RemoOnline"},
                    {"SecurityConfiguration:Key", "<?xml version=\"1.0\" encoding=\"utf-8\"?><key id=\"f919bd29-672d-466b-94b3-cbc4857e65b2\" version=\"1\">  <creationDate>2017-11-13T17:32:16.7656557Z</creationDate><activationDate>2017-11-13T17:32:16.707303Z</activationDate><expirationDate>2019-02-11T17:32:16.707303Z</expirationDate><descriptor deserializerType=\"Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel.AuthenticatedEncryptorDescriptorDeserializer, Microsoft.AspNetCore.DataProtection, Version=2.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60\"><descriptor><encryption algorithm=\"AES_256_CBC\" />\r\n      <validation algorithm=\"HMACSHA256\" /><masterKey p4:requiresEncryption=\"true\" xmlns:p4=\"http://schemas.asp.net/2015/03/dataProtection\">\r\n  <value>whHINBEGp3fnMlgR5d8RwmoPXDddtDvdsveoBOE+TXRPERNevqh39+/Ss0tE46dQCXWMw9BBeJ/SaFxGHbjE5w==</value></masterKey></descriptor></descriptor></key>"},
                    {"SecurityConfiguration:LoginPath", "/login"}
                }
            };
        }

        /// <summary>
        /// Gets the configuration for anti forgery token.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetConfigurationForAntiForgeryToken()
        {
            yield return new object[]
            {
                new Dictionary<string, string>
                {
                    {"SecurityConfiguration:AntiForgeryTokenHeader", "X-XSRF-TOKEN"}
                }
            };
        }

        /// <summary>
        /// Gets the security configurations.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetSecurityConfigurations()
        {
            yield return new object[]
            {
                new Dictionary<string, string>
                {
                    {"SecurityConfiguration:AntiForgeryTokenHeader", "X-XSRF-TOKEN"},
                    {"SecurityConfiguration:SecurityApplicationGroup", "RemoOnline"},
                    {"SecurityConfiguration:CorsPolicy:AllowWhiteListDomains", "[ \"*\" ]"},
                    {"SecurityConfiguration:CorsPolicy:AllowHeaders", "[ \"*\" ]"},
                    {"SecurityConfiguration:CorsPolicy:AllowMethods", "[ \"*\" ]"},
                    {"SecurityConfiguration:Key", "<?xml version=\"1.0\" encoding=\"utf-8\"?><key id=\"f919bd29-672d-466b-94b3-cbc4857e65b2\" version=\"1\">  <creationDate>2017-11-13T17:32:16.7656557Z</creationDate><activationDate>2017-11-13T17:32:16.707303Z</activationDate><expirationDate>2019-02-11T17:32:16.707303Z</expirationDate><descriptor deserializerType=\"Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel.AuthenticatedEncryptorDescriptorDeserializer, Microsoft.AspNetCore.DataProtection, Version=2.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60\"><descriptor><encryption algorithm=\"AES_256_CBC\" />\r\n      <validation algorithm=\"HMACSHA256\" /><masterKey p4:requiresEncryption=\"true\" xmlns:p4=\"http://schemas.asp.net/2015/03/dataProtection\">\r\n  <value>whHINBEGp3fnMlgR5d8RwmoPXDddtDvdsveoDOE+TXRPERNevqh39+/Ss0tE46dQCXWMw9BBeJ/SaFxGHbjE5w==</value></masterKey></descriptor></descriptor></key>"},
                    {"SecurityConfiguration:LoginPath", "/login"}
                }
            };
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
