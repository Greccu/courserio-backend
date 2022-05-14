using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courserio.Core.Enums;

namespace Courserio.Core.Models
{
    public class MlModel : BaseEntity
    {
        public MlModelTypeEnum Type { get; set; }
        public byte[] Content { get; set; }
    }
}
