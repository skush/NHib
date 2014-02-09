using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Model
{
    public class Product
    {
        public virtual int? ProductId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual UnitOfMeasure UOM { get; set; }
    }
}
