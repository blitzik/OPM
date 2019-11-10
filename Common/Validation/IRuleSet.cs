using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation
{
    public interface IRuleSet
    {
        /// <summary>
        /// If it's TRUE, validation stops right after an error occurs
        /// </summary>
        bool IsStrict { get; }

        List<IValidationMessage> Errors { get; }
    }
}
