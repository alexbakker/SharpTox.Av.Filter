using System;

namespace SharpTox.Av.Filter
{
	/// <summary>
	/// Represents an instance of filter_audio.
	/// </summary>
	public class FilterAudio : IDisposable
	{
		private bool _disposed;
		private int _sampleRate;
		private readonly FilterAudioHandle _filterAudio;

		/// <summary>
		/// The amount of time in ms it takes for audio to be played and recorded back after.
		/// </summary>
		public short EchoDelay
		{
			set
			{
				if (_disposed)
					throw new ObjectDisposedException(GetType().FullName);

				FilterAudioFunctions.SetEchoDelayMs(_filterAudio, value);
			}
		}

		private bool _echoFilterEnabled = true;
		private bool _voiceFilterEnabled = true;
		private bool _grainFilterEnabled = true;

		/// <summary>
		/// Whether or not echo filtering is enabled.
		/// </summary>
		/// <value></value>
		public bool EchoFilterEnabled
		{
			get
			{
				return _echoFilterEnabled;
			}
			set
			{
				if (_disposed)
					throw new ObjectDisposedException(GetType().FullName);

				_echoFilterEnabled = value;
				FilterAudioFunctions.EnableDisableFilters(_filterAudio, value ? 1 : 0, VoiceFilterEnabled ? 1 : 0, GrainFilterEnabled ? 1 : 0);
			}
		}

		/// <summary>
		/// Whether or not voice filtering is enabled.
		/// </summary>
		/// <value></value>
		public bool VoiceFilterEnabled
		{
			get
			{
				return _voiceFilterEnabled;
			}
			set
			{
				if (_disposed)
					throw new ObjectDisposedException(GetType().FullName);

				_voiceFilterEnabled = value;
				FilterAudioFunctions.EnableDisableFilters(_filterAudio, EchoFilterEnabled ? 1 : 0, value ? 1 : 0, GrainFilterEnabled ? 1 : 0);
			}
		}

		/// <summary>
		/// Whether or not grain filtering is enabled.
		/// </summary>
		/// <value></value>
		public bool GrainFilterEnabled
		{
			get
			{
				return _grainFilterEnabled;
			}
			set
			{
				if (_disposed)
					throw new ObjectDisposedException(GetType().FullName);

				_grainFilterEnabled = value;
				FilterAudioFunctions.EnableDisableFilters(_filterAudio, EchoFilterEnabled ? 1 : 0, VoiceFilterEnabled ? 1 : 0, value ? 1 : 0);
			}
		}

		/// <summary>
		/// The sample rate for this instance of FilterAudio.
		/// </summary>
		/// <value></value>
		public int SampleRate
		{
			get
			{
				return _sampleRate;
			}
		}

		/// <summary>
		/// Initialises a new instance of filter_audio.
		/// </summary>
		/// <param name="sampleRate"></param>
		public FilterAudio(int sampleRate)
		{
			_sampleRate = sampleRate;
			_filterAudio = FilterAudioFunctions.NewFilterAudio((uint)sampleRate);
		}

		/// <summary>
		/// Filters audio. (highpass filter, gain control)
		/// </summary>
		/// <param name="data"></param>
		/// <param name="samples">the amount of samples per frame</param>
		/// <returns></returns>
		public bool Filter(short[] data, int samples)
		{
			if (_disposed)
				throw new ObjectDisposedException(GetType().FullName);

			return FilterAudioFunctions.FilterAudio(_filterAudio, data, (uint)samples) == 0;
		}

		/// <summary>
		/// Makes sure echo gets canceled from the frame.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="samples">the amount of samples per frame</param>
		/// <returns></returns>
		public bool PassAudioOutput(short[] data, int samples)
		{
			if (_disposed)
				throw new ObjectDisposedException(GetType().FullName);

			return FilterAudioFunctions.PassAudioOutput(_filterAudio, data, (uint)samples) == 0;
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

			if (disposing)
			{
			}

			if (!_filterAudio.IsInvalid && !_filterAudio.IsClosed && _filterAudio != null)
				_filterAudio.Dispose();

			_disposed = true;
		}
	}
}
