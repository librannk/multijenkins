using System.Diagnostics.CodeAnalysis;

namespace SiteConfiguration.API.TransactionPriority.RequestResponseModel
{
    [ExcludeFromCodeCoverage]
    public class SmartSortResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

    }
}
