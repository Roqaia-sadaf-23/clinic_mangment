using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Common.Exceptions
{

    public sealed class ConflictException : Exception
    {
        public ConflictException(string message)
            : base(message)
        {

        }
    }
}