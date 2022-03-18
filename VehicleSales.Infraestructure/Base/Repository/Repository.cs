using System;
using System.Collections.Generic;
using System.Linq;
using VehicleSales.Infraestructure.Base.Context;
using VehicleSales.Model.Base.Exception;
using VehicleSales.Model.Base.Model;
using VehicleSales.Model.Base.Repository;

namespace VehicleSales.Infraestructure.Base.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        private VehicleSalesContext _myDbContext;


        public Repository(VehicleSalesContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public void Add(T entity)
        {
            try
            {
                _myDbContext.Add(entity);
                _myDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }
        }

        public void AddRange(List<T> entities)
        {
            try
            {
                _myDbContext.AddRange(entities);
                _myDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }
        }

        public virtual void Delete(int id)
        {
            try
            {
                var entity = _myDbContext.Set<T>().FirstOrDefault(x => x.Id == id);

                if (entity != null)
                {
                    _myDbContext.Remove(entity);
                    _myDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }
        }

        public virtual IList<T> FindAll()
        {
            try
            {
                return _myDbContext.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }

        }

        public virtual T FindById(int id)
        {
            try
            {
                return _myDbContext.Set<T>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }

        }

        public virtual void Update(T entity)
        {
            try
            {
                _myDbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _myDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }
        }

        public virtual T FindBy(Func<T, bool> predicate)
        {
            try
            {
                return _myDbContext.Set<T>().FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }

        }
    }
}
