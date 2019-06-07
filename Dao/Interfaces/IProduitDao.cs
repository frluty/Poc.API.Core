using Poc.API.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.API.Core.Dao.Interfaces
{
    public interface IProduitDao
    {
        IEnumerable<ProduitDto> GetList(int nb);

        ProduitDto GetProduit(long id);

        bool CreateProduit(ProduitDto Dto);

        bool DeleteProduit(long id);

        bool UpdateProduit(ProduitDto Dto, long id);
    }
}
