using System.Threading.Tasks;

namespace AnimFlex.Sequencer.UserEnd {
    public static class Helpers {
        
        /// <summary>
        /// Awaits the completion of the sequence
        /// </summary>
        public static async Task AwaitComplete(this Sequence sequence) {
            await Task.Yield(); // first frame it won't be playing
            while(sequence.IsPlaying()) await Task.Yield();
        }
    }
}