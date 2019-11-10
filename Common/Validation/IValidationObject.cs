using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation
{
    public interface IValidationObject : INotifyDataErrorInfo
    {
        Dictionary<string, IRuleSet> RuleSets { get; }
        Dictionary<string, List<IValidationMessage>> Errors { get; }

        void AddRuleSet<P>(string propertyName, IDelegateRuleSet<P> ruleSet);

        bool Validate<T>(T obj, [CallerMemberName]string propertyName = "");
        bool Check<T>(T obj, [CallerMemberName]string propertyName = "");

        void RaiseErrorsChanged(string propertyName);
    }
}
