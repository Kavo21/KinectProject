using System;
using System.IO;
using System.Media;
using System.Threading;
using CognitiveServicesTTS;

namespace Kinect_v2_Learning
{
    public class Pronunciation
    {
        private string accessToken;
        private string requestUri = "https://eastasia.tts.speech.microsoft.com/cognitiveservices/v1";
        private Authentication auth;
      
        public Pronunciation()
        {
            InitializeTextToSpeech();
        }

        private void InitializeTextToSpeech()
        {
            this.auth = new Authentication("https://eastasia.api.cognitive.microsoft.com/sts/v1.0/issuetoken", "19a1cda4bdc24063b2a3b14031c6edf1");
            Console.WriteLine("Starting Authtentication");

            try
            {
                accessToken = auth.GetAccessToken();
                Console.WriteLine("Token: {0}\n", accessToken);
            }
            catch (Exception ex)
            {
                //c7ecf0e0f1b74c16a44defa1fe24261a Free use
                //848c666465d245599984559b40eebfc7
                Console.WriteLine("Failed authentication.");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
                return;
            }
        }

        public void SetText(String Text)
        {    
            var cortana = new Synthesize();
            cortana.OnAudioAvailable += PlayAudio;
            cortana.OnError += ErrorHandler;

            // Reuse Synthesize object to minimize latency
            cortana.Speak(CancellationToken.None, new Synthesize.InputOptions()
            {
                RequestUri = new Uri(requestUri),
                // Text to be spoken.
                //Text = "Hello, how are you doing?",
                //Text = "This method is called once the audio returned from the service"
                Text = Text,
                VoiceType = Gender.Female,
                // Refer to the documentation for complete list of supported locales.
                Locale = "en-US",
                // You can also customize the output voice. Refer to the documentation to view the different
                // voices that the TTS service can output.
                //VoiceName = "Microsoft Server Speech Text to Speech Voice (en-US, Jessa24KRUS)",
                VoiceName = "Microsoft Server Speech Text to Speech Voice (en-US, Guy24KRUS)",
                //VoiceName = "Microsoft Server Speech Text to Speech Voice (en-US, ZiraRUS)",

                // Service can return audio in different output format.
                OutputFormat = AudioOutputFormat.Riff24Khz16BitMonoPcm,
                AuthorizationToken = "Bearer " + accessToken,
            });
        }

        /// <summary>
        /// This method is called once the audio returned from the service.
        /// It will then attempt to play that audio file.
        /// Note that the playback will fail if the output audio format is not pcm encoded.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="GenericEventArgs{Stream}"/> instance containing the event data.</param>
        private void PlayAudio(object sender, GenericEventArgs<Stream> args)
        {
            Console.WriteLine(args.EventData);

            // For SoundPlayer to be able to play the wav file, it has to be encoded in PCM.
            // Use output audio format AudioOutputFormat.Riff16Khz16BitMonoPcm to do that.
            SoundPlayer player = new SoundPlayer(args.EventData);
            player.PlaySync();
            args.EventData.Dispose();
        }

        /// <summary>
        /// Handler an error when a TTS request failed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GenericEventArgs{Exception}"/> instance containing the event data.</param>
        private void ErrorHandler(object sender, GenericEventArgs<Exception> e)
        {  
            Console.WriteLine("Unable to complete the TTS request: [{0}]", e.ToString());
            InitializeTextToSpeech();
        }
    }
}