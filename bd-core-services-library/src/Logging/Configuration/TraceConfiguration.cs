using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Core.Logging.Configuration
{
    /// <summary>
    /// Trace Configuration 
    /// </summary>
    public class TraceConfiguration
    {
        public bool Enabled { get; set; }
        public string ExcludePattern { get; set; }
    }
}
