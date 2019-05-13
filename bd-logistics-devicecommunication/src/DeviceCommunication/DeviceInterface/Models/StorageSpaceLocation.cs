
namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Models
{
    /// <summary>
    /// Class Isa
    /// </summary>
    public class Isa
    {
        /// <summary>
        /// read-write property ShortDescription
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// read-write property DisplayArrowDirection
        /// </summary>
        public char? DisplayArrowDirection { get; set; }
        /// <summary>
        /// read-write property IsFiniteOffset
        /// </summary>
        public bool? IsFiniteOffset { get; set; }
        /// <summary>
        /// read-write property IsStatic
        /// </summary>
        public bool? IsStatic { get; set; }
        /// <summary>
        /// read-write property MaxShelf
        /// </summary>
        public int? MaxShelf { get; set; }
        /// <summary>
        /// read-write property MaxRack
        /// </summary>
        public int? MaxRack { get; set; }
        /// <summary>
        /// read-write property MaxDisplayColumns
        /// </summary>
        public int? MaxDisplayColumns { get; set; }
    }
    /// <summary>
    /// Rack class
    /// </summary>
    public class Rack
    {
        /// <summary>
        /// read-write property RackNum
        /// </summary>
        public int? RackNum { get; set; }
        /// <summary>
        /// read-write property RackNum
        /// </summary>
        public Isa ISA { get; set; }    
    }

    /// <summary>
    ///  class Shelf
    /// </summary>
    public class Shelf
    {
        /// <summary>
        /// read-write property OverideBaseAddr
        /// </summary>
        public int? OverideBaseAddr { get; set; }
        /// <summary>
        /// read-write property ShelfNum
        /// </summary>
        public int? ShelfNum { get; set; }
        /// <summary>
        /// read-write property Rack
        /// </summary>
        public Rack Rack { get; set; }
        /// <summary>
        /// read-write property NextShelf
        /// </summary>
        public Shelf NextShelf { get; set; }
        /// <summary>
        /// read-write property PreviousShelf
        /// </summary>
        public Shelf PreviousShelf { get; set; }
    }

    /// <summary>
    /// class Bin
    /// </summary>
    public class Bin
    {
        /// <summary>
        /// read-write property BinNum
        /// </summary>
        public int? BinNum { get; set; }
        /// <summary>
        /// read-write property LeftOffset
        /// </summary>
        public decimal? LeftOffset { get; set; }
        /// <summary>
        /// read-write property Shelf
        /// </summary>
        public Shelf Shelf { get; set; }
    }

    /// <summary>
    /// class Slot
    /// </summary>

    public class Slot
    {
        /// <summary>
        /// read-write property SlotNum
        /// </summary>
        public int? SlotNum { get; set; }
        /// <summary>
        /// read-write property DispenseForm
        /// </summary>
        public string DispenseForm { get; set; }
        /// <summary>
        /// read-write property QtyOnHand
        /// </summary>
        public int QtyOnHand { get; set; }
        /// <summary>
        /// read-write property QtyMin
        /// </summary>
        public int QtyMin { get; set; }
        /// <summary>
        /// read-write property QtyMax
        /// </summary>
        public int QtyMax { get; set; }
        /// <summary>
        /// read-write property QtyInPrepack
        /// </summary>
        public int QtyInPrepack { get; set; }
        /// <summary>
        /// read-write property QtyPrepackFixed
        /// </summary>
        public int QtyPrepackFixed { get; set; }
        /// <summary>
        /// read-write property ReplenishmentSlotId
        /// </summary>
        public int? ReplenishmentSlotId { get; set; }
        /// <summary>
        /// read-write property OrderFromDistributor
        /// </summary>
        public bool OrderFromDistributor { get; set; }
        /// <summary>
        /// read-write property Bin
        /// </summary>
        public Bin Bin { get; set; }
    }
}

