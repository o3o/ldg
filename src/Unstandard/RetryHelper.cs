//http://www.danharman.net/2011/06/13/a-retry-helper-class-with-exponential-backoff/
namespace Microline {
   internal class RetryFunc<T> {
      internal T Value {
         get;
         private set;
      }


      /**
       * Performs an operations returning a type of T. Should the operation throw an exception, it will be
       * retried after the retry interval. The backoff is exponentional on each retry.
       *
       *    string res = new RetryFunc<string>( () => myFunc(myParam1, myParam2), 5, 500).Value;
       *
       * @param func" The function delegate.</param>
       * @param maxAttempts Number of times to retry the operation.</param>
       * @param baseMillisecondsInterval The interval of the first retry, will double upon each attempt.</param>
       */
      internal RetryFunc(System.Func<T> func, int maxAttempts = 5, int baseMillisecondsInterval = 500) {
         int retryInterval = baseMillisecondsInterval;

         for (int attempt = 1; attempt <= maxAttempts; ++attempt) {
            try {
               Value = func();
               return;
            } catch (System.Exception) {
               //O-FIXME:
               //if (log.IsErrorEnabled) log.Error(string.Format(
                        //"Failure executing operation (Attempt={0}) : {1}"
                        //, attempt
                        //, ex.Message));
            }

            System.Threading.Thread.Sleep(retryInterval);

            retryInterval *= 2;
         }

         throw new System.ApplicationException(string.Format("Exhausted retries (Attempts={0})", maxAttempts));
      }
   }

   /*
    *  new RetryAction( () => myFunc(myParam1, myParam2), 5, 500);
    */
   internal class RetryAction {
      internal RetryAction(System.Action func, int maxAttempts = 5, int baseMillisecondsInterval = 500) {
         int retryInterval = baseMillisecondsInterval;

         for (int attempt = 1; attempt <= maxAttempts; ++attempt) {
            try {
               func();
               return;
            } catch (System.Exception) {
               //O-FIXME: if (log.IsErrorEnabled) log.Error(string.Format(
                        //"Failure executing operation (Attempt={0}) : {1}"
                        //, attempt
                        //, ex.Message));
            }

            System.Threading.Thread.Sleep(retryInterval);

            retryInterval *= 2;
         }

         throw new System.ApplicationException(string.Format("Exhausted retries (Attempts={0})", maxAttempts));
      }
   }
}
