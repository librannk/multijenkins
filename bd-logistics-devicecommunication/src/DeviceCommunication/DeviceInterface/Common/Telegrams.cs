
namespace Logistics.Services.DeviceCommunication.API.DeviceInterface.Common
{
    using System.Text;
    /// <summary>
    /// class type Telegrams
    /// </summary>
    public class Telegrams
    {       
        #region Static methods

        /// <summary>
        /// Convertion from string to byte array
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string s)
        {
            int length = s.Length;
            byte[] buffer = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = (byte)s[i];
            }
            return buffer;
        }

        /// <summary>
        ///  Conversion from byte array to string
        /// </summary>
        /// <param name="data"> byte array</param>
        /// <returns>string</returns>
        public static string GetString(byte[] data)
        {
            int length = data.GetLength(0);
            StringBuilder builder = new StringBuilder
            {
                Length = 0
            };
            for (int i = 0; i < length; i++)
            {
                builder.Append((char)data[i]);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Conversion from byte array to string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startAt"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetString(byte[] data, int startAt, int count)
        {
            data.GetLength(0);
            StringBuilder builder = new StringBuilder
            {
                Length = 0
            };
            for (int i = startAt; i < (startAt + count); i++)
            {
                builder.Append((char)data[i]);
            }
            return builder.ToString();
        }

        #endregion
    }
}

