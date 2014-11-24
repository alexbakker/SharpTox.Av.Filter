using System;
using System.Runtime.InteropServices;

namespace SharpTox.Av.Filter
{
    internal static class FilterAudioFunctions
    {
        const string dllName = "filter_audio.dll";

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "new_filter_audio")]
        internal static extern FilterAudioHandle NewFilterAudio(uint fs);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "kill_filter_audio")]
        internal static extern void KillFilterAudio(IntPtr filterAudio);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "filter_audio")]
        internal static extern int FilterAudio(FilterAudioHandle filterAudio, short[] data, uint samples);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pass_audio_output")]
        internal static extern int PassAudioOutput(FilterAudioHandle filterAudio, short[] data, uint samples);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "set_echo_delay_ms")]
        internal static extern int SetEchoDelayMs(FilterAudioHandle filterAudio, short msInSndCardBuf);
    }
}
