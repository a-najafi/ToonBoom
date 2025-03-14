namespace ToonBoomCore.Utility
{
    using System.Threading.Tasks;
    using DG.Tweening;

    public static class DOTweenExtensions
    {
        public static Task AsyncWaitForCompletion(this Sequence sequence)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            sequence.OnComplete(() => tcs.SetResult(true));
            return tcs.Task;
        }
    }

}