

//TODO: To be refactored 
namespace StorageSpace.API.Model
{

    /// <summary>
    /// Comp.
    /// </summary>
    public class Comp
    {

        /// <summary>
        /// Gets or sets a value indicating whether this instance is outside computer.
        /// </summary>
        /// <value><c>true</c> if this instance is outside computer; otherwise, <c>false</c>.</value>
        public bool IsOutsideComputer { get; set; }



        /// <summary>
        /// Gets or sets the computer key.
        /// </summary>
        /// <value>The computer key.</value>
        public int ComputerKey { get; set; }


        /// <summary>
        /// Gets or sets the printer key.
        /// </summary>
        /// <value>The printer key.</value>
        public int PrinterKey { get; set; }


        /// <summary>
        /// Gets or sets the device number.
        /// </summary>
        /// <value>The device number.</value>
        public string DeviceNumber { get; set; }


        /// <summary>
        /// Gets or sets the display base left offset.
        /// </summary>
        /// <value>The display base left offset.</value>
        public int DisplayBaseLeftOffset { get; set; }
    }
}
