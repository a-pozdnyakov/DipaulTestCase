using ReactiveUI;
using Splat;
using System;

namespace DipaulTestCase.Converters
{
    public class BoolToReverseBoolConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            return fromType == typeof(bool)
                ? 1
                : 0;
        }

        public bool TryConvert(object? from, Type toType, object? conversionHint, out object? result)
        {
            try
            {
                result = from is bool value
                    ? !value
                    : from;
            }
            catch (Exception ex)
            {
                this.Log().Warn(ex, "Couldn't convert object to type: {}", toType);

                result = null;
                
                return false;
            }

            return true;
        }
    }
}