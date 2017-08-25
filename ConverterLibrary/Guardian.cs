using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterLibrary
{
    class Guardian
    {
        public readonly int BaseNumberOfCalls;
        public int NumberOfCallsLeft { get; private set; }
        public event Action Action;
        public Guardian(int maxNumberOfCalls, Action actonToCall = null)
        {
            BaseNumberOfCalls = maxNumberOfCalls;
            Action += actonToCall;
            ResetNumberOfCalls();
        }
        public void Call()
        {
            if (--NumberOfCallsLeft < 0)
            {
                Action?.Invoke();
            }
        }

        public void ResetNumberOfCalls()
        {
            NumberOfCallsLeft = BaseNumberOfCalls;
        }
    }
}
