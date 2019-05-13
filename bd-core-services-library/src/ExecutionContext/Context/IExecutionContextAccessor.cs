
namespace BD.Core.Context
{
    /// <summary> Contract for creating execution accessor </summary>
    public interface IExecutionContextAccessor
    {
        Context Current { get; set; }
    }
}
