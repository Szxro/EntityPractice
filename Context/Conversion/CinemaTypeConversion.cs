using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Conversion
{
    public class CinemaTypeConversion : ValueConverter<CinemaType, string>
    {
        private static readonly Dictionary<CinemaType, string> _ConversionToString = new()
        {
            {CinemaType.ThreeDimension,"ThreeDimension"},
            { CinemaType.TwoDimension,"TwoDimension"}
        };

        private static readonly Dictionary<string, CinemaType> _ConversionToCinemaType = new()
        {
            {"ThreeDimension",CinemaType.ThreeDimension},
            { "TwoDimension",CinemaType.TwoDimension}
        };

        public CinemaTypeConversion() :base(
                value => CinemaTypeToString(value),// One is used to send to the DB from Enum to String (Post)
                value => StringToCinemaType(value)// Other is used to translate from String to Enum (Get)
            )
        {
            
        }

        private static string CinemaTypeToString(CinemaType type)
        {
             return _ConversionToString[type] ?? "Not Defined";
        }

        private static CinemaType StringToCinemaType(string type)
        {
            return _ConversionToCinemaType[type];
        }
    }
}
