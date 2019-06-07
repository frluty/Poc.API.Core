using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.API.Core.Maps
{
    public class ProduitMap: ClassMap<Models.Produit>
    {
        public ProduitMap()
        {
            Table("Produits");
            LazyLoad();
            Id( x => x.Id).Column("ID").GeneratedBy.Assigned().Unique();
            Map(x => x.Name).Column("Nom");
        }
    }
}
