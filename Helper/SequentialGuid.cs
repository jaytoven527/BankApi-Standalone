using System.Collections;

namespace BankingApi_with_ReactFrontend.Server.Helper
{
    public static class SequentialGuid
    {
        public static Guid NewSequentialGuid()
        {
            var DestinationByteArray = Guid.NewGuid().ToByteArray();

            long Timestamp = DateTime.UtcNow.Ticks;
            var SourceByteArray = BitConverter.GetBytes(Timestamp);

            Array.Copy(SourceByteArray, DestinationByteArray, 8);

            return new Guid(DestinationByteArray);

        }
    }
}
