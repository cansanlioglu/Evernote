﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Core.DataAccess
{
    public interface IDataAccess<T> 
    {
        List<T> List();
        List<T> List(Expression<Func<T, bool>>where); //x=> x.ID==3
        IQueryable<T> ListQueryable();

        int Insert(T obj);
        int Update(T obj);
        int Delete(T obj);
        int Save();
        T Find(Expression<Func<T, bool>> where);
    }
}
