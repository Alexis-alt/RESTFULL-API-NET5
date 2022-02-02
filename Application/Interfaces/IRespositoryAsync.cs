using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
   public  interface IRespositoryAsync<T>:IReadRepositoryBase<T> where T: class
    {




    }

    interface IReadRepositoryAsync<T>: IReadRepositoryBase<T> where T :class
    {


    }



}
