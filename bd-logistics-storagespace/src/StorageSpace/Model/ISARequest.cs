
namespace StorageSpace.API.Model
{
    /// <summary>
    /// ISARequest
    /// </summary>
    public class ISARequest
    {

        /// <summary>
        /// Gets or sets the isa Id.
        /// </summary>
        /// <value>The isa Id.</value>
        public string IsaId { get; set; }


        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }


        /// <summary>
        /// Gets or sets the type of the isa.
        /// </summary>
        /// <value>The type of the isa.</value>
        public string IsaType { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ISARequest"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active { get; set; }
    }
}
