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
            Id( x => x.Id).Column("ID").GeneratedBy.Identity();
            Map(x => x.Name).Column("Nom");
            Map(x => x.Prix).Column("Prix");
            Map(x => x.Categorie).Column("Categorie");
        }
    }
}
