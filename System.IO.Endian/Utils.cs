﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.IO.Endian
{
    internal static class Utils
    {
        private static readonly Dictionary<string, Attribute> attrVerCache = new Dictionary<string, Attribute>();
        private static readonly HashSet<string> propValidationCache = new HashSet<string>();

        internal static T GetAttributeForVersion<T>(MemberInfo member, double? version) where T : Attribute, IVersionAttribute
        {
            var key = member is TypeInfo 
                ? $"{typeof(T).Name}|{((TypeInfo)member).FullName}:{version}" 
                : $"{typeof(T).Name}|{member.DeclaringType.FullName}.{member.Name}:{version}";

            if (attrVerCache.ContainsKey(key))
                return (T)attrVerCache[key];

            var matches = Utils.GetCustomAttributes<T>(member).Where(o =>
            {
                var minVersion = o.HasMinVersion ? o.MinVersion : (double?)null;
                var maxVersion = o.HasMaxVersion ? o.MaxVersion : (double?)null;

                //exclude when read version is specified and is out of bounds (this expression will always be false if version is null)
                if ((version != minVersion && (version < minVersion || version >= maxVersion)))
                    return false;

                //exclude when read version is not specified but at least one of the bounds is
                if (!version.HasValue && (minVersion.HasValue || maxVersion.HasValue))
                    return false;

                return true;
            }).ToList();

            if (matches.Count > 1)
            {
                //if there is a versioned match and an unversioned match we should use the versioned match
                if (matches.Count == 2)
                {
                    matches = matches.Where(o => o.HasMinVersion || o.HasMaxVersion).ToList();
                    if (matches.Count == 1)
                    {
                        attrVerCache.Add(key, matches.Single());
                        return matches.Single();
                    }
                    //else both or neither are versioned: fall through to the error below
                }

                throw Exceptions.AttributeVersionOverlap(member.Name, typeof(T).Name, version);
            }

            attrVerCache.Add(key, matches.FirstOrDefault());
            return matches.FirstOrDefault();
        }

        internal static bool CheckPropertyForReadWrite(PropertyInfo property, double? version)
        {
            var key = $"{property.DeclaringType.FullName}.{property.Name}:{version}";
            if (propValidationCache.Contains(key)) return true;

            if (!Attribute.IsDefined(property, typeof(OffsetAttribute)))
                return false; //ignore properties with no offset assigned

            if (Attribute.IsDefined(property, typeof(VersionSpecificAttribute))
                || Attribute.IsDefined(property, typeof(MinVersionAttribute))
                || Attribute.IsDefined(property, typeof(MaxVersionAttribute)))
            {
                if (!version.HasValue)
                    return false; //ignore versioned properties if no read version is specified

                var single = Utils.GetCustomAttribute<VersionSpecificAttribute>(property);
                var min = Utils.GetCustomAttribute<MinVersionAttribute>(property);
                var max = Utils.GetCustomAttribute<MaxVersionAttribute>(property);

                //must satisfy any and all version restrictions that are applied

                if (version != (single?.Version ?? version))
                    return false;

                if (version < min?.MinVersion)
                    return false;

                if (version >= max?.MaxVersion && version != min?.MinVersion)
                    return false;
            }

            if (Utils.GetAttributeForVersion<OffsetAttribute>(property, version) == null)
                throw Exceptions.NoOffsetForVersion(property.Name, version);

            propValidationCache.Add(key);
            return true;
        }

        internal static T GetCustomAttribute<T>(MemberInfo member) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(member, typeof(T));
        }

        internal static IEnumerable<T> GetCustomAttributes<T>(MemberInfo member) where T : Attribute
        {
            return Attribute.GetCustomAttributes(member, typeof(T)).OfType<T>();
        }
    }
}
