﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{

    //Clase encargada de paginar los registros que se entregan como respuesta al cliente

   public class PagedResponse<T>:Response<T> 
    {


        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {

            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;

                


        }





    }
}
