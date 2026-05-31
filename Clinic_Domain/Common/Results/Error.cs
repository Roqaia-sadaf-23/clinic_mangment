using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Domain.Common.Results
{
    public readonly record struct Error
    {

        private Error(string code, string description,ErrorKind type) 
        { 
        Code = code;
            Description = description;
            Type = type;

        
        }

        public string Code { get; }
        public string Description { get; }
        public ErrorKind Type { get; }


        public static Error Failure(string code = nameof(Failure), string description = "General Failure")
            => new(code,description,ErrorKind.Failure);


        public static Error Unexpected(string code = nameof(Unexpected), string description = "Unexpected")
            => new(code, description, ErrorKind.Unexpected);

        public static Error Validation(string code = nameof(Validation), string description = "Validation Error")
            => new(code, description, ErrorKind.Validation);


        public static Error Conflict(string code = nameof(Conflict), string description = "Conflict Error")
            => new(code, description, ErrorKind.Conflict);


        public static Error NotFound(string code = nameof(NotFound), string description = "Not Found Error")
            => new(code, description, ErrorKind.NotFound);

        public static Error Unauthored(string code = nameof(Unauthored), string description = "Unauthored")
            => new(code, description, ErrorKind.Unauthored);

        public static Error Forbidden(string code = nameof(Forbidden), string description = "Forbidden")
           => new(code, description, ErrorKind.Forbidden);

        public static Error Create(int Type,string code , string description )
      => new(code, description,(ErrorKind)Type);

    }
}
