using System;
using nkast.Wasm.Dom;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class BaseAudioContext : JSObject
    {
        AudioDestinationNode _destination;
        AudioListener _listener;

        public BaseAudioContext(int uid) : base(uid)
        {
        }

        public int SampleRate
        {
            get
            {
                int sampleRate = InvokeRetInt("nkAudioBaseContext.GetSampleRate");
                return sampleRate;
            }
        }

        public AudioDestinationNode Destination
        {
            get
            {
                if (_destination == null)
                {
                    int uid = InvokeRetInt("nkAudioBaseContext.GetDestination");
                    _destination = new AudioDestinationNode(uid, this);
                }

                return _destination;
            }
        }

        public AudioListener Listener
        {
            get
            {
                if (_listener == null)
                {
                    int uid = InvokeRetInt("nkAudioBaseContext.GetListener");
                    _listener = new AudioListener(uid, this);
                }

                return _listener;
            }
        }

        public AudioWorklet AudioWorklet
        {
            get
            {
                int uid = InvokeRetInt("nkAudioBaseContext.GetAudioWorklet");
                AudioWorklet audioWorklet = AudioWorklet.FromUid(uid);
                if (audioWorklet != null)
                    return audioWorklet;

                return new AudioWorklet(uid);
            }
        }

        public ContextState State
        {
            get { return (ContextState)InvokeRetInt("nkAudioBaseContext.GetState"); }
        }

        public AudioBuffer CreateBuffer(int numOfChannels, int  length, int sampleRate)
        {
            int uid = InvokeRetInt<int, int, int>("nkAudioBaseContext.CreateBuffer", numOfChannels, length, sampleRate);
            return new AudioBuffer(uid, this);
        }

        public AudioBufferSourceNode CreateBufferSource()
        {
            int uid = InvokeRetInt("nkAudioBaseContext.CreateBufferSource");
            return new AudioBufferSourceNode(uid, this);
        }

        public OscillatorNode CreateOscillator()
        {
            int uid = InvokeRetInt("nkAudioBaseContext.CreateOscillator");
            return new OscillatorNode(uid, this);
        }        

        public MediaElementAudioSourceNode CreateMediaElementSource(IHTMLMediaElement media)
        {
            int uid = InvokeRetInt<int>("nkAudioBaseContext.CreateMediaElementSource", ((JSObject)media).Uid);
            return new MediaElementAudioSourceNode(uid, this, media);
        }

        public GainNode CreateGain()
        {
            int uid = InvokeRetInt("nkAudioBaseContext.CreateGain");
            return new GainNode(uid, this);
        }

        public StereoPannerNode CreateStereoPanner()
        {
            int uid = InvokeRetInt("nkAudioBaseContext.CreateStereoPanner");
            return new StereoPannerNode(uid, this);
        }

        public AudioWorkletNode CreateWorklet(string name)
        {
            int uid = InvokeRetInt("nkAudioBaseContext.CreateWorklet", name);
            return new AudioWorkletNode(uid, this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}
