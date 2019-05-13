using Facility.API.Constants;

namespace Facility.API.Model.InternalModels
{
    /// <summary>
    /// Wrapper class for creation result with creation result and created object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessResult<T>
    {
        /// <summary>
        /// Gets or sets the created object.
        /// </summary>
        /// <value>The object.</value>
        public T Object { get; }

        /// <summary>
        /// Gets or sets the creation result.
        /// </summary>
        /// <value>The creation result.</value>
        public CreateUpdateResultEnum OperationResult { get; }

        /// <summary>
        /// Gets the result count.
        /// </summary>
        /// <value>The result count.</value>
        public int ResultCount { get; }

        /// <summary>
        /// Gets or sets the error message if any.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessResult{T}"/> class.
        /// </summary>
        /// <param name="obj">The created object.</param>
        /// <param name="operationResult">The creation result.</param>
        /// <param name="errorMessage">The error message.</param>
        public BusinessResult(T obj, CreateUpdateResultEnum operationResult, string errorMessage = "")
        {
            Object = obj;
            OperationResult = operationResult;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessResult{T}"/> class.
        /// </summary>
        /// <param name="obj">The created object.</param>
        /// <param name="operationResult">The creation result.</param>
        /// <param name="resultCount">Number of total results in result-set.</param>
        public BusinessResult(T obj, CreateUpdateResultEnum operationResult, int resultCount)
        :this(obj,operationResult)
        {
            Object = obj;
            OperationResult = operationResult;
            ResultCount = resultCount;
        }
    }
}