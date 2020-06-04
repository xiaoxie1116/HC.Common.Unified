using HC.Common.Base.DTO;
using HC.Common.Base.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HC.Common.Base.Services
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        public IBaseRepository<TEntity> BaseDal = new BaseRepository<TEntity>();

        //public IBaseRepository<TEntity> BaseDal;//通过在子类的构造函数中注入，这里是基类，不用构造函数      

        public async Task<TEntity> QueryById(object objId)
        {            
            return await BaseDal.QueryById(objId);
        }

        /// <summary>
        /// 功能描述:根据ID查询一条数据        
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            return await BaseDal.QueryById(objId, blnUseCache);
        }

        /// <summary>
        /// 功能描述:根据ID查询数据        
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await BaseDal.QueryByIDs(lstIds);
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            return await BaseDal.Add(entity);
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            return await BaseDal.Update(entity);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Update(List<TEntity> entity)
        {
            return await BaseDal.Update(entity);
        }

        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await BaseDal.Update(entity, strWhere);
        }

        public async Task<bool> Update(
         TEntity entity,
         List<string> lstColumns = null,
         List<string> lstIgnoreColumns = null,
         string strWhere = ""
            )
        {
            return await BaseDal.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }


        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {
            return await BaseDal.Delete(entity);
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            return await BaseDal.DeleteById(id);
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await BaseDal.DeleteByIds(ids);
        }


        /// <summary>
        /// 功能描述:查询所有数据        
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await BaseDal.Query();
        }

        /// <summary>
        /// 功能描述:查询数据列表        
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await BaseDal.Query(strWhere);
        }

        /// <summary>
        /// 功能描述:查询数据列表        
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseDal.Query(whereExpression);
        }

        /// <summary>
        /// 功能描述:查询一个列表        
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await BaseDal.Query(whereExpression, orderByExpression, isAsc);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await BaseDal.Query(whereExpression, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询一个列表        
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await BaseDal.Query(strWhere, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询前N条数据        
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await BaseDal.Query(whereExpression, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询前N条数据        
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            string strWhere,
            int intTop,
            string strOrderByFileds)
        {
            return await BaseDal.Query(strWhere, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:分页查询        
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex,
            int intPageSize,
            string strOrderByFileds)
        {
            return await BaseDal.Query(
              whereExpression,
              intPageIndex,
              intPageSize,
              strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:分页查询        
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
          string strWhere,
          int intPageIndex,
          int intPageSize,
          string strOrderByFileds)
        {
            return await BaseDal.Query(
            strWhere,
            intPageIndex,
            intPageSize,
            strOrderByFileds);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件</param>
        /// <param name="intPageIndex">页数</param>
        /// <param name="intPageSize">条数</param>
        /// <param name="strOrderByFileds">排序(比如： " Id desc ")</param>
        /// <returns></returns>
        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await BaseDal.QueryPage(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }

        /// <summary>
        /// 多表联合查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new()
        {
            return await BaseDal.QueryMuch(joinExpression, selectExpression, whereLambda);
        }

        /// <summary>
        /// 原生Sql查询
        /// </summary>
        /// <param name="SqlName"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> SqlQuery(string SqlName)
        {
            return await BaseDal.SqlQuery(SqlName);
        }

        /// <summary>
        /// 执行原生的sql存储过程
        /// </summary>
        /// <param name="ProcName">存储过程名称</param>
        /// <param name="parames">存储过程所需参数</param>
        /// <returns></returns>
        public async Task<int> ExecuteProcedure(string ProcName, params KeyValues[] parames)
        {
            return await BaseDal.ExecuteProcedure(ProcName, parames);
        }

        /// <summary>
        /// 原生的sql存储过程查询
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="parames"></param>
        /// <returns></returns>
        public async Task<DataTable> ExecuteProcedureQuery(string ProcName, params KeyValues[] parames)
        {
            return await BaseDal.ExecuteProcedureQuery(ProcName, parames);
        }




    }
}
