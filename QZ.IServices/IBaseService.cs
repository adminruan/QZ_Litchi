using QZ.Models.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace QZ.IServices
{
    public interface IBaseService
    {
        List<T> GetList<T>() where T : class;

        T Create<T>(T entity) where T : class;
    }
}
