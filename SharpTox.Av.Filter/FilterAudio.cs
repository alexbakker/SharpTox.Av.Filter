using System;

namespace SharpTox.Av.Filter
{
    public class FilterAudio : IDisposable
    {
        private bool _disposed;
        private int _sampleRate;
        private FilterAudioHandle _filterAudio;

        public FilterAudio(int sampleRate)
        {
            _sampleRate = sampleRate;
            _filterAudio = FilterAudioFunctions.NewFilterAudio((uint)sampleRate);
        }

        public bool Filter(short[] data, int samples)
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);

            return FilterAudioFunctions.FilterAudio(_filterAudio, data, (uint)samples) == 0;
        }

        /// <summary>
        /// Releases all resources used by this instance of filter_audio.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing) { }

            if (!_filterAudio.IsInvalid && !_filterAudio.IsClosed && _filterAudio != null)
                _filterAudio.Dispose();

            _disposed = true;
        }
    }
}
