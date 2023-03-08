using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class MovieGenderDTO
    {
        //FlexMap
        private string NewName { get; set; } = null!;
        public string Name
        {
            get //name is going to work with newName
            {
                return NewName;
            }
            set
            {
                NewName = value.ToLower();
                //value is the actual value that is set to newName 
            }
        }

        // public string Name { get; set; } = null!;
    }
}
