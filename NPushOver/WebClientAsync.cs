using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NPushover
{
   public static class WebClientAsync
   {
      /// <summary>
      /// Completes the Task if the user state matches the TaskCompletionSource.
      /// </summary>
      /// <typeparam name="T">Specifies the type of data returned by the Task.</typeparam>
      /// <param name="tcs">The TaskCompletionSource.</param>
      /// <param name="e">The completion event arguments.</param>
      /// <param name="requireMatch">Whether we require the tcs to match the e.UserState.</param>
      /// <param name="getResult">A function that gets the result with which to complete the task.</param>
      /// <param name="unregisterHandler">An action used to unregister work when the operaiton completes.</param>
      public static void HandleEapCompletion<T>(
          TaskCompletionSource<T> tcs, bool requireMatch, AsyncCompletedEventArgs e, Func<T> getResult, Action unregisterHandler)
      {
         // Transfers the results from the AsyncCompletedEventArgs and getResult() to the
         // TaskCompletionSource, but only AsyncCompletedEventArg's UserState matches the TCS
         // (this check is important if the same WebClient is used for multiple, asynchronous
         // operations concurrently).  Also unregisters the handler to avoid a leak.
         if (!requireMatch || e.UserState == tcs)
         {
            try { unregisterHandler(); }
            finally
            {
               if (e.Cancelled) tcs.TrySetCanceled();
               else if (e.Error != null) tcs.TrySetException(e.Error);
               else tcs.TrySetResult(getResult());
            }
         }
      }


      public static Task<byte[]> UploadValuesTaskAsync(this WebClient wc, Uri uri, NameValueCollection parameters)
      {
         // Create the task to be returned
         var tcs = new TaskCompletionSource<byte[]>(uri);

         // Setup the callback event handler
         UploadValuesCompletedEventHandler completedHandler = null;
         completedHandler = (sender, e) => HandleEapCompletion(tcs, true, e, () => e.Result, () => wc.UploadValuesCompleted -= completedHandler);

         wc.UploadValuesCompleted += completedHandler;

         // Start the async operation.
         try
         {
            wc.UploadValuesAsync(uri, "POST", parameters, tcs);
         }
         catch
         {
            wc.UploadValuesCompleted -= completedHandler;
            throw;
         }

         // Return the task that represents the async operation
         return tcs.Task;
      }
   }
}
