using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BD.Core.BaseModels
{
    /// <summary> Use this payload resoponse class to handle the paginated response. </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ResponsePayload{T}" />
    public class PaginatedResponsePayload<T> : ResponsePayload<T>
    {
        /// <summary> Gets or sets the response. </summary>
        /// <value> The response. </value>
        [DataMember(Name = "responsePayload")]
        public new IList<T> Response { get; set; }

        /// <summary> Gets or sets the record count. </summary>
        /// <value> The record count. </value>
        [DataMember]
        public int RecordCount { get; set; }

        /// <summary> Gets or sets the current page. </summary>
        /// <value> The current page. </value>
        [DataMember]
        public int CurrentPage { get; set; }

        /// <summary> Gets or sets the next. </summary>
        /// <value> The next. </value>
        [DataMember]
        public string Next { get; set; }

        /// <summary> Gets or sets the previous. </summary>
        /// <value> The previous. </value>
        [DataMember]
        public string Previous { get; set; }

        /// <summary> Gets or sets the size of the page. </summary>
        /// <value> The size of the page. </value>
        [DataMember]
        public string PageSize { get; set; }

        /// <summary> Returns a <see cref="string" /> that represents this instance. </summary>
        /// <returns> A <see cref="string" /> that represents this instance. </returns>
        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(Response)}: {Response}, " +
                   $"{nameof(RecordCount)}: {RecordCount}, {nameof(CurrentPage)}: " +
                   $"{CurrentPage}, {nameof(Next)}: {Next}, {nameof(Previous)}: " +
                   $"{Previous}, {nameof(PageSize)}: {PageSize}";
        }
    }
}
