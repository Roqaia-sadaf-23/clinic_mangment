using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Domain.Common
{
    public abstract class AuditableEntity: Entity
    {
     
            
            protected AuditableEntity() { }
       
        protected AuditableEntity(int id):base(id)
        {
        }
      

        public DateTime CreateAt { get; set; }

        public String? CreatedBy { get; set; }

        public DateTime  LastModifiedUtc { get; set; }

        public String? LastModifiedBy { get; set; }
    }
}
