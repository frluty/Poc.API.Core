using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.API.Core.Dto
{
    public class ProduitDto: EntityBase
    {
        public virtual string Name { get; set; }
        public virtual string Categorie { get; set; }
        public virtual float Prix { get; set; }
    }
}
