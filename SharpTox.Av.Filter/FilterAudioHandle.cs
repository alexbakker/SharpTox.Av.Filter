using System;
using System.Runtime.InteropServices;

namespace SharpTox.Av.Filter
{
    internal class FilterAudioHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private FilterAudioHandle()
            : base(true) { }

        protected override bool ReleaseHandle()
        {
            FilterAudioFunctions.KillFilterAudio(handle);
            return true;
        }
    }

    internal abstract class SafeHandleZeroOrMinusOneIsInvalid : SafeHandle
    {
        protected SafeHandleZeroOrMinusOneIsInvalid(bool ownsHandle)
            : base(IntPtr.Zero, ownsHandle)
        {
        }

        public override bool IsInvalid
        {
            get { return handle == IntPtr.Zero || handle == new IntPtr(-1); }
        }
    }
}
