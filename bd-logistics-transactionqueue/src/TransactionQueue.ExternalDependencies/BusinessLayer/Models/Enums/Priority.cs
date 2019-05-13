
namespace TransactionQueue.ExternalDependencies.BusinessLayer.Models.Enums
{
    /// <summary>
    /// Transaction Priority Enums
    /// </summary>
    public enum Priority
    {
        /// <summary> PYXISCRITLOW = 0 </summary>
        PYXISCRITLOW,
        /// <summary> PYXISSTOCKOUT = 1 </summary>
        PYXISSTOCKOUT,
        /// <summary> PYXISSTKOUT = 2 </summary>
        PYXISSTKOUT,
        /// <summary> PYXISLOAD = 3 </summary>
        PYXISLOAD,
        /// <summary> PYXISREFILL = 4 </summary>
        PYXISREFILL,
        /// <summary> STAT = 5 </summary>
        STAT
    }
}
