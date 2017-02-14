using OA.DAL;
using OA.DALFactory;
using OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
   public abstract class BaseService<T> where T : class, new()
    {
        public IDBSession CurrentDBSession
        {
            get
            {
                return DBSessionFactory.CreateDBSession();
            }
        }

        public IDAL.IBaseDal<T> CurrentDal { get; set; }
        public abstract void SetCurrentDal();
        public BaseService()
        {
            SetCurrentDal();  //抽象方法的子类一定要实现的方法
        }


        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.LoadEntities(whereLambda);
        }


        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            return CurrentDal.LoadPageEntities<s>(pageIndex, pageSize, out totalCount, whereLambda, orderbyLambda, isAsc);
        }
        public bool DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
            return CurrentDBSession.SaveChange();
        }
        public bool EditEntity(T entity)
        {
            CurrentDal.EditEntity(entity);
            return CurrentDBSession.SaveChange();
        }

        public T AddEntity(T entity)
        {
            CurrentDal.AddEntity(entity);
            CurrentDBSession.SaveChange();
            return entity;
        }
    }

  
}
