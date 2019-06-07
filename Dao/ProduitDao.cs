using Microsoft.Extensions.Logging;
using Poc.API.Core.Dao.Interfaces;
using Poc.API.Core.Dto;
using Poc.API.Core.Exceptions;
using Poc.API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.API.Core.Dao
{
    public class ProduitDao : IProduitDao
    {
        private IRepository<ProduitDto> _repository;
        readonly ILogger<ProduitDao> _logger;

        public ProduitDao(IRepository<ProduitDto> repository, ILogger<ProduitDao> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }
     
       public ProduitDto GetProduit(long id)
        {
            try
            {
                return _repository.GetById(id);
            }
            catch (ApiException e)
            {
                this._logger.LogError(e.Message);
                return null;
            }
        }

        public bool CreateProduit(ProduitDto Dto)
        {
            try
            {
                
                return _repository.Create(Dto);
            }
            catch (ApiException e)
            {
                this._logger.LogError(e.Message);
                return false;
            }
        }

        public bool DeleteProduit(long id)
        {
            try
            {
              return  _repository.Delete(id);
            }catch(ApiException e)
            {
                this._logger.LogError(e.Message);
                return false;
            }
        }
        public bool UpdateProduit(ProduitDto Dto, long id)
        {
            try
            {
                return _repository.Update(Dto, id);
            }
            catch (ApiException e)
            {
                this._logger.LogError(e.Message);
                return false;
            }
        }

        public IEnumerable<ProduitDto> GetList(int nb)
        {
            try
            {
                return _repository.GetAll(nb);
            }
            catch (ApiException e)
            {
                this._logger.LogError(e.Message);
                return null;
            }
        }
    }
}
