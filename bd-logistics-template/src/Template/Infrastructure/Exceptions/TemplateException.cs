using System;

namespace BD.Template.API.Infrastructure.Exceptions
{
    /// <summary>
    /// TemplateException
    /// </summary>
    public class TemplateException: Exception
    {
        /// <summary>
        /// TemplateException
        /// </summary>
        public TemplateException()
        {
        }

        /// <summary>
        /// TemplateException
        /// </summary>
        /// <param name="message"></param>
        public TemplateException(string message) : base(message)
        {
        }


       
    }
}
