using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;
using UserNotifications.Applications.Utilities.Abstract;

namespace UserNotifications.Applications.Utilities.Implementation
{
    internal class EnumParserUtility : IEnumParserUtility
    {
        public IReadOnlyCollection<EnumInfoDto> Parse<T>() where T : struct, Enum
        {
            var result = new List<EnumInfoDto>();

            foreach (var value in Enum.GetValues<T>())
            {
                var fieldInfo = typeof(T).GetField(value.ToString());
                var descriptionAttribute = fieldInfo?
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;

                result.Add(new EnumInfoDto
                {
                    Id = Convert.ToInt32(value),
                    Description = descriptionAttribute?.Description ?? value.ToString()
                });
            }

            return result;
        }
    }
}
