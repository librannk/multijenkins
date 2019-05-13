namespace StorageSpace.API.Model
{
    /// <summary> Computer </summary>
    public class Computer
    {
        /// <summary> ComputerId </summary>
        public int ComputerId  {get;set;}

        /// <summary> PrinterId </summary>
        public int PrinterId           {get;set;}

        /// <summary> IsOutsideComputer </summary>
        public bool IsOutsideComputer   {get;set;}
    }
}
