using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Domain.Entites
{
    public class UserDocument
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
