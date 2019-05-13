using MongoDB.Bson;
using System.Collections.Generic;

namespace StorageSpace.API.Model
{ 

    /// <summary>
    /// ISA.
    /// </summary>
    public class ISA
    {
        /// <summary>
        /// Gets or sets the isa identifier.
        /// </summary>
        /// <value>The isa identifier.</value>
        public string IsaId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [active flag].
        /// </summary>
        /// <value><c>true</c> if [active flag]; otherwise, <c>false</c>.</value>
        public bool ActiveFlag { get; set; }

        /// <summary>
        /// Gets or sets the short description.
        /// </summary>
        /// <value>The short description.</value>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the transaction queue lock expiration.
        /// </summary>
        /// <value>The transaction queue lock expiration.</value>
        public int TransactionQueueLockExpiration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [req restock lot information flag].
        /// </summary>
        /// <value><c>true</c> if [req restock lot information flag]; otherwise, <c>false</c>.</value>
        public bool ReqRestockLotInfoFlag { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [static flag].
        /// </summary>
        /// <value><c>true</c> if [static flag]; otherwise, <c>false</c>.</value>
        public bool StaticFlag { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [return status flag].
        /// </summary>
        /// <value><c>true</c> if [return status flag]; otherwise, <c>false</c>.</value>
        public bool ReturnStatusFlag { get; set; }

        /// <summary>
        /// Gets or sets the carousel.
        /// </summary>
        /// <value>The carousel.</value>
        public ObjectId Carousel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [disconnect on idle flag].
        /// </summary>
        /// <value><c>true</c> if [disconnect on idle flag]; otherwise, <c>false</c>.</value>
        public bool DisconnectOnIdleFlag { get; set; }

        /// <summary>
        /// Gets or sets the device number.
        /// </summary>
        /// <value>The device number.</value>
        public int DeviceNumber { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the connection reset minutes.
        /// </summary>
        /// <value>The connection reset minutes.</value>
        public int ConnectionResetMinutes { get; set; }

        /// <summary>
        /// Gets or sets the maximum rack.
        /// </summary>
        /// <value>The maximum rack.</value>
        public int MaxRack { get; set; }

        /// <summary>
        /// Gets or sets the maximum bin.
        /// </summary>
        /// <value>The maximum bin.</value>
        public int MaxBin { get; set; }

        /// <summary>
        /// Gets or sets the default width of the bin.
        /// </summary>
        /// <value>The default width of the bin.</value>
        public int DefaultBinWidth { get; set; }

        /// <summary>
        /// Gets or sets the maximum shelves.
        /// </summary>
        /// <value>The maximum shelves.</value>
        public int MaxShelves { get; set; }

        /// <summary>
        /// Gets or sets the width of the shelf.
        /// </summary>
        /// <value>The width of the shelf.</value>
        public int ShelfWidth { get; set; }

        /// <summary>
        /// Gets or sets the default bin dividers h.
        /// </summary>
        /// <value>The default bin dividers h.</value>
        public int DefaultBinDividersH { get; set; }

        /// <summary>
        /// Gets or sets the default bin dividers v.
        /// </summary>
        /// <value>The default bin dividers v.</value>
        public int DefaultBinDividersV { get; set; }

        /// <summary>
        /// Gets or sets the maximum display columns.
        /// </summary>
        /// <value>The maximum display columns.</value>
        public int MaxDisplayColumns { get; set; }

        /// <summary>
        /// Gets or sets the display columns per inch.
        /// </summary>
        /// <value>The display columns per inch.</value>
        public int DisplayColumnsPerInch { get; set; }

        /// <summary>
        /// Gets or sets the display arrow direction.
        /// </summary>
        /// <value>The display arrow direction.</value>
        public string DisplayArrowDirection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [display attached flag].
        /// </summary>
        /// <value><c>true</c> if [display attached flag]; otherwise, <c>false</c>.</value>
        public bool DisplayAttachedFlag { get; set; }

        /// <summary>
        /// Gets or sets the display type key.
        /// </summary>
        /// <value>The display type key.</value>
        public string DisplayTypeKey { get; set; }

        /// <summary>
        /// Gets or sets the display ip address.
        /// </summary>
        /// <value>The display ip address.</value>
        public string DisplayIPAddress { get; set; }

        /// <summary>
        /// Gets or sets the display port.
        /// </summary>
        /// <value>The display port.</value>
        public int DisplayPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [restrict control flag].
        /// </summary>
        /// <value><c>true</c> if [restrict control flag]; otherwise, <c>false</c>.</value>
        public bool RestrictControlFlag { get; set; }

        /// <summary>
        /// Gets or sets the computers.
        /// </summary>
        /// <value>The computers.</value>
        public List<Comp> Computers { get; set; }
    }
}
