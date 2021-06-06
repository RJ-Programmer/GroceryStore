using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Domain.Common
{
    public class AutitableEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
