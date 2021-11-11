using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Introductions
{
    public enum IntroductionStatus
     {
        PENDING_APPROVAL=0,
        APPROVAL_ACCEPTED=1,
        APPROVAL_DECLINED=2,
        ACCEPTED=3,
        DECLINED=4
     }
}