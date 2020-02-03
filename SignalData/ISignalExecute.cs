using System;
using System.Collections.Generic;
using System.Text;
using DOT.AGM.Client;
using DOT.AGM.Services;

namespace SignalData
{
    // add a method namespace - so all methods will be prefixed with Glue42.Samples.
    [ServiceContract(MethodNamespace = "Glue42.Samples.")]
    public interface ISignalExecute : IDisposable
    {
        // since the method does not have an output, we can go with async invocation
        [ServiceOperation(AsyncIfPossible = true, ExceptionSafe = true)]
        void ExecuteSignal(TradeSignal input);

        // since we have an output, we have to explicitly say that we want a single target, and not all
        [ServiceOperation(InvocationTargetType = MethodTargetType.Any)]
        bool ExecuteSignal2(TradeSignal input, out TradeSignal updated);

    }
}
