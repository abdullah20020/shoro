using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomianLayar.Enums
{
    public enum CaseStatus
    {
        Open = 1,           
        UnderReview = 2,    
        Investigation = 3,  
        InCourt = 4,        
        JudgmentIssued = 5, 
        Closed = 6,         
        Suspended = 7,      
        Appealed = 8,       
        Settled = 9         
    }
}
