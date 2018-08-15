using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Shared
{
    public class BusinessException : Exception
    {
        public BusinessException(string Messagem) : base(Messagem)
        {

        }
    }
}