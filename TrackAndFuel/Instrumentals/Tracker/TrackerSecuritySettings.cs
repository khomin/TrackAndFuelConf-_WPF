using System.Runtime.InteropServices;

namespace TrackAndFuel.Instrumentals.Tracker
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TrackerSecuritySettings
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Password;
    }
}
