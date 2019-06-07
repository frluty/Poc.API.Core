using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.API.Core.Models
{
    public class Produit:ModelBase
    {
        public virtual string Name { get; set; }
        public virtual string Categorie { get; set; }
        public virtual float Prix { get; set; }
    }
}
