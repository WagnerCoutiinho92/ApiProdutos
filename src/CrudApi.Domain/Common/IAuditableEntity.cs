using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudApi.Domain.Common
{
    public interface IAuditableEntity
    {
        DateTime? DeletedAt { get; set; }
    }
}
