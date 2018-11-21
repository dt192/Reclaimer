﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.IO.Endian
{
    internal static class Exceptions
    {
        private static string ToCurrent(FormattableString formattable)
        {
            if (formattable == null)
                throw new ArgumentNullException(nameof(formattable));

            return formattable.ToString(CultureInfo.CurrentCulture);
        }

        #region Generic Errors

        internal static ArgumentOutOfRangeException ParamMustBePositive(string paramName, object paramValue)
        {
            return new ArgumentOutOfRangeException(paramName, paramValue, ToCurrent($"The {paramName} value must be greater than zero."));
        }

        internal static ArgumentOutOfRangeException ParamMustBeNonNegative(string paramName, object paramValue)
        {
            return new ArgumentOutOfRangeException(paramName, paramValue, ToCurrent($"The {paramName} value must be non-negative."));
        }

        internal static ArgumentOutOfRangeException BoundaryOverlapAmbiguous(string minValue, string maxValue)
        {
            return new ArgumentOutOfRangeException(ToCurrent($"The {minValue} value cannot be greater than the {maxValue} value."));
        }

        internal static ArgumentOutOfRangeException BoundaryOverlapMinimum(string minValue, string maxValue)
        {
            return new ArgumentOutOfRangeException(ToCurrent($"{minValue} cannot be greater than {maxValue}."));
        }

        internal static ArgumentOutOfRangeException BoundaryOverlapMaximum(string minValue, string maxValue)
        {
            return new ArgumentOutOfRangeException(ToCurrent($"{maxValue} cannot be less than {minValue}."));
        }

        internal static ArgumentOutOfRangeException PropertyMustBeNullOrPositive(string paramName, object paramValue)
        {
            return new ArgumentOutOfRangeException(paramName, paramValue, ToCurrent($"The {paramName} property must either be null or greater than zero."));
        }

        #endregion

        #region Specific Errors

        internal static MissingMethodException MissingPrimitiveReadMethod(string typeName)
        {
            return new MissingMethodException(ToCurrent($"{nameof(EndianReader)} does not have a primitive read function for {typeName} values."));
        }

        internal static MissingMethodException MissingPrimitiveWriteMethod(string typeName)
        {
            return new MissingMethodException(ToCurrent($"{nameof(EndianWriter)} does not have a primitive write function for {typeName} values."));
        }

        internal static MissingMethodException TypeNotConstructable(string typeName, bool isProperty)
        {
            if (isProperty)
                return new MissingMethodException(ToCurrent($"A property of type '{typeName}' was marked for read/write but '{typeName}' does not have a public default constructor."));
            else
                return new MissingMethodException(ToCurrent($"Cannot create an object of type '{typeName}' because '{typeName}' does not have a public default constructor."));
        }

        internal static MissingMethodException NonPublicGetSet(string propName)
        {
            return new MissingMethodException(ToCurrent($"The '{propName}' property was marked for read/write but has no public get and/or set methods."));
        }

        internal static AmbiguousMatchException AttributeVersionOverlap(string memberName, string attrName, double? version)
        {
            return new AmbiguousMatchException(ToCurrent($"The type or property '{memberName}' has multiple {attrName}s specified that are a match for version '{version?.ToString(CultureInfo.CurrentCulture) ?? "null"}'."));
        }

        internal static AmbiguousMatchException StringTypeOverlap(string propName)
        {
            return new AmbiguousMatchException(ToCurrent($"The '{propName}' string property has multiple string type specifier attributes applied."));
        }

        internal static InvalidOperationException StringTypeUnknown(string propName)
        {
            return new InvalidOperationException(ToCurrent($"The '{propName}' string property was marked for read/write but does not have any string type attributes set."));
        }

        internal static AmbiguousMatchException MultipleVersionsSpecified(string typeName)
        {
            return new AmbiguousMatchException(ToCurrent($"The object of type '{typeName}' could not be read because it has multiple properties with the {nameof(VersionNumberAttribute)} applied."));
        }

        internal static InvalidOperationException NoOffsetForVersion(string propName, double? version)
        {
            return new InvalidOperationException(ToCurrent($"The property '{propName}' has no offset specified for version '{version?.ToString(CultureInfo.CurrentCulture) ?? "null"}'. If the property is not applicable for this version, apply the {nameof(VersionSpecificAttribute)} with appropriate parameters."));
        }

        internal static ArgumentException InvalidVersionAttribute()
        {
            return new ArgumentException(ToCurrent($"The property with the {nameof(VersionNumberAttribute)} applied must have a single offset supplied and no version restrictions."));
        }

        internal static ArgumentException NotValidForPrimitiveTypes([CallerMemberName]string methodName = null)
        {
            return new ArgumentException(ToCurrent($"{methodName} should not be used on primitive types or strings."));
        }

        internal static InvalidCastException PropertyNotConvertable(string propName, string storeType, string propType)
        {
            return new InvalidCastException(ToCurrent($"The property '{propName}' has a {nameof(StoreTypeAttribute)} value of '{storeType}' but '{storeType}' could not be converted to/from '{propType}'."));
        }

        internal static ArgumentOutOfRangeException OutOfStreamBounds(string paramName, object value)
        {
            return new ArgumentOutOfRangeException(paramName, value, ToCurrent($"The {paramName} value is out of bounds. The value must be non-negative and no greater than the length of the underlying stream."));
        }

        #endregion
    }
}
