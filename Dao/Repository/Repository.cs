using AutoMapper;
using NHibernate;
using Poc.API.Core.Data;
using Poc.API.Core.Dto;
using Poc.API.Core.Exceptions;
using Poc.API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Poc.API.Core.Dao.Repository
{
    public class Repository<S, T> : Interfaces.IRepository<T> where T : EntityBase where S : ModelBase
    {
        private readonly ISessionFactory session;

        public Repository(FluentNHibernateHelper fluentNHibernateHelper = null)
        {
            session = FluentNHibernateHelper.sessionFactory;
        }

        public bool Create(T entity)
        {

            using (var con = session.OpenSession())
            {
                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        var elt = Mapper.Map<S>(entity);

                        con.Save(elt);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        throw (new ApiException(e.Message));
                    }
                }
            }

            return true;
        }

        public bool Delete(long id)
        {
            try
            {
                using (var con = session.OpenSession())
                {
                    using (var transaction = con.BeginTransaction())
                    {
                        var elt = con.Query<S>().FirstOrDefault(a => a.Id == id);
                        con.DeleteAsync(elt);
                        transaction.CommitAsync();
                    }
                }
            }

            catch (Exception e)
            {
                throw (new ApiException(e.Message));
            }

            return true;
        }

        public IEnumerable<T> GetAll(int nb)
        {
            List<T> allData = new List<T>();
            try
            {
                using (var con = session.OpenSession())
                {
                    var data = con.Query<S>().Take(nb).ToList();
                    foreach (var item in data)
                    {
                        allData.Add(Mapper.Map<T>(item));
                    }
                }

            }
            catch (Exception e)
            {
                throw (new ApiException(e.Message));
            }
            return allData;
        }

        public IEnumerable<T> GetAll()
        {
            List<T> allData = new List<T>();
            try
            {
                using (var con = session.OpenSession())
                {
                    var data = con.Query<S>().ToList();
                    foreach (var item in data)
                    {
                        allData.Add(Mapper.Map<T>(item));
                    }
                }

            }
            catch (Exception e)
            {
                throw (new ApiException(e.Message));
            }
            return allData;
        }

        public T GetById(long id)
        {
            T elt = null;
            try
            {
                using (var con = session.OpenSession())
                {
                    var data = con.Query<S>().FirstOrDefault(a => a.Id == id);
                    elt = Mapper.Map<T>(data);
                }

            }
            catch (Exception e)
            {
                throw (new ApiException(e.Message));
            }
            return elt;
        }

        public bool Update(T entity, long id)
        {
            try
            {
                using (var con = session.OpenSession())
                {
                    using (var transaction = con.BeginTransaction())
                    {
                        var data = con.Query<S>().FirstOrDefault(a => a.Id == id);
                        var elt = Mapper.Map<T, S>(entity, data);
                        con.UpdateAsync(elt);
                        transaction.CommitAsync();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                throw (new ApiException(e.Message));
            }
        }

    }
}
