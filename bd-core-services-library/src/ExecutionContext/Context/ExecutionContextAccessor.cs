using System.Threading;

namespace BD.Core.Context
{
    /// <summary> ExecutionContextAccessor to give current context </summary>
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private static AsyncLocal<ExecutionContextHolder> _executionContextCurrent = 
            new AsyncLocal<ExecutionContextHolder>();

        public Context Current
        {
            get { return _executionContextCurrent.Value?.Context; }

            set
            {
                var holder = _executionContextCurrent.Value;
                if (holder != null)
                {
                    // Clear current ExectionContext trapped in the AsyncLocals.
                    holder.Context = null;
                }

                if (value != null)
                {
                    // Use an object indirection to hold the ExectionContext in the AsyncLocal,
                    // so it can be cleared in all ExecutionContexts when its cleared.
                    _executionContextCurrent.Value = new ExecutionContextHolder { Context = value };
                }
            }
        }
        private class ExecutionContextHolder
        {
            public Context Context;
        }
    }
}
