using System.Threading.Tasks;

namespace GeoPic.Helpers
{
    /// <summary>
    /// Class TaskUtils.
    /// </summary>
    public static class TaskUtils
    {
        /// <summary>
        /// Tasks from result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">The result.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public static Task<T> TaskFromResult<T>(T result)
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetResult(result);
            return tcs.Task;
        }
    }
}