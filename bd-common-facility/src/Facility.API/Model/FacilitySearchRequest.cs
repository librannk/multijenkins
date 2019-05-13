namespace Facility.API.Model
{
    /// <summary>
    /// Model for searching Facility
    /// </summary>
    public class FacilitySearchRequest
    {
        /// <summary>
        /// Search Term that should be searched.
        /// </summary>
        /// <value>The search term.</value>
        public string SearchTerm { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether search should be done in in-active facilities.
        /// </summary>
        /// <value><c>true</c> if search should be done in in-active facilities; otherwise, <c>false</c>.</value>
        public bool SearchInInactive { get; set; }
    }
}
