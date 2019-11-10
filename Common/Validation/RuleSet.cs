using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation
{
    public class RuleSet<T> : IDelegateRuleSet<T>
    {
        private List<IValidationMessage> _errors;
        public List<IValidationMessage> Errors
        {
            get { return _errors; }
        }


        /// <summary>
        /// If it's TRUE, validation stops right after an error occurs
        /// </summary>
        private bool _isStrict;
        public bool IsStrict
        {
            get { return _isStrict; }
            private set { _isStrict = value; }
        }


        private List<IRule<T>> _rules;

        public RuleSet(bool isStrict = false) : base()
        {
            _errors = new List<IValidationMessage>();
            _rules = new List<IRule<T>>();
            IsStrict = isStrict;
        }


        public RuleSet<T> AddRule(Func<T, bool> rule, string message, Severity severity = Severity.WARNING)
        {
            _rules.Add(new Rule<T>(rule, message, severity));
            return this;
        }


        public RuleSet<T> AddRule(IRule<T> rule)
        {
            _rules.Add(rule);
            return this;
        }


        public bool Check(T obj)
        {
            Errors.Clear();
            foreach (IRule<T> rule in _rules) {
                if (!rule.Check(obj)) {
                    Errors.Add(rule.Error);
                    if (IsStrict) {
                        return false;
                    }
                }
            }

            if (Errors.Count > 0) {
                return false;
            }
            return true;
        }
    }
}
