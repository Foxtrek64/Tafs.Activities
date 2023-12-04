using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Tafs.Activities.SecureStringConverter;

namespace Tafs.Activities.Extensions.Statements
{
    /// <summary>
    /// Converts the provided string into a SecureString.
    /// </summary>
    [DisplayName("String As SecureString")]
    [Description("Provides an activity to convert a string variable to a SecureString.")]
    public sealed class AsSecureString : CodeActivity<SecureString>
    {
        /// <summary>
        /// Gets or sets the input string.
        /// </summary>
        [RequiredArgument]
        [DefaultValue("")]
        [Description("The string to convert into a SecureString.")]
        public InArgument<string> Input { get; set; } = new();

        /// <inheritdoc/>
        protected override SecureString Execute(CodeActivityContext context)
        {
            var input = Input.Get(context);

            if (string.IsNullOrEmpty(input))
            {
                return default;
            }
            return input.AsSecureString();
        }
    }
}
