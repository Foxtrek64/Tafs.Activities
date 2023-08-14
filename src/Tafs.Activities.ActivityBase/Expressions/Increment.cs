using System;
using System.Activities;
using System.Activities.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tafs.Activities.ActivityBase.Extensions;

namespace Tafs.Activities.ActivityBase.Expressions
{
    /// <summary>
    /// A code activity which increments a numeral.
    /// </summary>
    /// <typeparam name="TNumeral">A numeric value, such as <see langword="int"/> or <see langword="long"/>.</typeparam>
    public sealed class Increment<TNumeral> : CodeActivity<TNumeral>
    {
        private static Func<TNumeral, TNumeral> operationFunction = null!;

        /// <summary>
        /// Gets or sets the numeral value that will be incremented.
        /// </summary>
        [RequiredArgument]
        [DefaultValue(null)]
        public InOutArgument<TNumeral> Numeral { get; set; } = null!;

        /// <inheritdoc/>
        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            BinaryExpressionHelper.OnGetArguments<TNumeral>(metadata, (InArgument<TNumeral>)Numeral);

            Increment<TNumeral>.EnsureOperationFunction(metadata, ref operationFunction, ExpressionType.Increment);
        }

        private static void EnsureOperationFunction(CodeActivityMetadata metadata, ref Func<TNumeral, TNumeral> operationFunction, ExpressionType operatorType)
        {
            if (operationFunction == null)
            {
                if (!BinaryExpressionHelper.TryGenerateUnaryDelegate(operatorType, out operationFunction, out ValidationError? validationError))
                {
                    metadata.AddValidationError(validationError);
                }
            }
        }

        /// <inheritdoc/>
        protected override TNumeral Execute(CodeActivityContext context)
        {
            TNumeral value = Numeral.Get(context);
            return operationFunction.Invoke(value);
        }
    }
}
