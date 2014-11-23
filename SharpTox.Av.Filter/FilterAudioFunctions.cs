using System;
using System.Runtime.InteropServices;

namespace SharpTox.Av.Filter
{
    internal class FilterAudioFunctions
    {
        const string dllName = "filter_audio.dll";

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "new_filter_audio")]
        internal static extern FilterAudioHandle NewFilterAudio(uint fs);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "kill_filter_audio")]
        internal static extern void KillFilterAudio(IntPtr filterAudio);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "filter_audio")]
        internal static extern int FilterAudio(FilterAudioHandle filterAudio, short[] data, uint samples);
    }
}
