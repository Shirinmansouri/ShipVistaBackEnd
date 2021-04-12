using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plants.Utility
{
    public static class Mapper
    {
        public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        {
            try
            {
                var sourceProps = source.GetType().GetProperties().Where(x => x.CanRead).ToList();

                if (typeof(T).GetInterfaces().Length > 0)
                    sourceProps.AddRange(typeof(T).GetInterfaces()[0].GetProperties());

                var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

                if (typeof(TU).GetInterfaces().Length > 0)
                    destProps.AddRange(typeof(TU).GetInterfaces()[0].GetProperties());

                foreach (var sourceProp in sourceProps)
                {
                    if (destProps.Any(x => (x.Name.ToLower() == sourceProp.Name.ToLower())
                                           && x.MemberType == sourceProp.MemberType))
                    {
                        var p = destProps.First(x => x.Name.ToLower() == sourceProp.Name.ToLower());
                        if (p.CanWrite)
                        {
                            // check if the property can be set or no.
                            p.SetValue(dest, sourceProp.GetValue(source, null), null);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception($"Error in CopyPropertiesTo method from {source} to  {dest}. {exp.Message}");
            }
        }
    }
}
