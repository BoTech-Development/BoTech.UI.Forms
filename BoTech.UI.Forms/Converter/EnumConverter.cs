using System;
using System.Collections.Generic;
using System.Text;

namespace BoTech.UI.Forms.Converter
{
    public class EnumConverter<TSourceEnum, TDestinationEnum> : IConverter<TSourceEnum, TDestinationEnum> 
        where TSourceEnum : struct, Enum 
        where TDestinationEnum : struct, Enum
    {
        public static TDestinationEnum Convert(TSourceEnum source)
        {
            return Enum.Parse<TDestinationEnum>(source.ToString());
        }

        public static TSourceEnum ConvertBack(TDestinationEnum source)
        {
            return Enum.Parse<TSourceEnum>(source.ToString());
        }
    }
}
