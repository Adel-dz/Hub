using System.IO;
using System.Text;

namespace easyLib
{
    public interface IStorable
    {
        void Read(IReader reader);
        void Write(IWriter writer);
    }

    public static class Storables
    {
        public static byte[] GetBytes(this IStorable storable)
        {
            var ms = new MemoryStream();
            var writer = new RawDataWriter(ms , Encoding.UTF8);

            storable.Write(writer);

            return ms.ToArray();
        }

        public static void SetBytes(this IStorable storable, byte[] data)
        {
            var ms = new MemoryStream(data);
            var reader = new RawDataReader(ms , Encoding.UTF8);

            storable.Read(reader);
        }
    }
}
