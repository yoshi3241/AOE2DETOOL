using Microsoft.CognitiveServices.Speech;
using System.Timers;
using AOE2DETOOL.Definition;

namespace AOE2DETOOL.Utilities
{
    internal static class Speech
    {
        private static List<string> _talkList = new List<string>();
        private static System.Timers.Timer? _talkTimer = null;

        public static void SpeechStart()
        {
            if (_talkTimer == null)
            {
                _talkTimer = new System.Timers.Timer(500);
                _talkTimer.Elapsed += SpeechTimeCore;
            }
            _talkTimer.Start();
        }

        private static async Task<bool> TalkProc(string speecText)
        {
            var subscriptionKey = Environment.GetEnvironmentVariable(Constants.KEY_ENV_AZURE_SPEECH) ?? "";
            var region = Environment.GetEnvironmentVariable(Constants.KEY_ENV_AZURE_SPEECH_REGION) ?? "";
            var SpeechSynthesisVoiceName = Environment.GetEnvironmentVariable(Constants.KEY_ENV_AZURE_SPEECH_VOICE) ?? "";

            var speechConfig = SpeechConfig.FromSubscription(subscriptionKey, region);

            // The language of the voice that speaks.
            speechConfig.SpeechSynthesisVoiceName = SpeechSynthesisVoiceName;

            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {

                //var ssml = File.ReadAllText("./Config/Ssml.xml");
                //var result = await speechSynthesizer.SpeakSsmlAsync(ssml);

                var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(speecText);

                OutputSpeechSynthesisResult(speechSynthesisResult, speecText);

            }

            return true;
        }

        public static async void SpeechTimeCore(object? sender, ElapsedEventArgs e)
        {
            if (_talkTimer is null || _talkList.Count == 0) return;

            _talkTimer.Stop();

            await TalkProc(_talkList[0]);

            if(_talkList.Count > 0)
            {
                _talkList.RemoveAt(0);
            }

            _talkTimer.Start();
        }

        public static async Task<bool> Talk(string speecText)
        {
            _talkList.Add(speecText);

            return true;
        }


        static void OutputSpeechSynthesisResult(SpeechSynthesisResult speechSynthesisResult, string text)
        {
            switch (speechSynthesisResult.Reason)
            {
                case ResultReason.SynthesizingAudioCompleted:
                    Console.WriteLine($"Speech synthesized for text: [{text}]");
                    break;
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
