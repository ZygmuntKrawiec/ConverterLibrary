using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterLibrary
{
    /// <summary>
    /// Represents a guardian which reacts by invoking an event after calling it.
    /// </summary>
    public class Guardian
    { 
        private readonly int BaseNumberOfCalls;
        /// <summary>
        /// Returns the number of calls left.
        /// </summary>
        public int NumberOfCallsLeft { get; private set; }
        private event Action Action;

        /// <summary>
        /// Initializes a new instance of the Guardian class.
        /// </summary>
        /// <param name="maxNumberOfCalls">A maximum number of calls the guardian</param>
        /// <param name="actionToCall">An action to invoke if the number of calls reached the maximum.</param>
        public Guardian(int maxNumberOfCalls, Action actionToCall = null)
        {
            BaseNumberOfCalls = maxNumberOfCalls;
            Action += actionToCall;
            ResetNumberOfCalls();
        }

        /// <summary>
        /// Adds one call. 
        /// </summary>
        public void Call()
        {
            if (--NumberOfCallsLeft < 0)
            {
                Action?.Invoke();
            }
        }

        /// <summary>
        /// Resets the number of calls.
        /// </summary>
        public void ResetNumberOfCalls()
        {
            NumberOfCallsLeft = BaseNumberOfCalls;
        }
    }
}
