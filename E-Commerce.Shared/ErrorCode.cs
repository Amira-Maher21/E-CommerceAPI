using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared
{
    public enum ErrorCode
    {
        None = 0,
        UnKnown = 1,

        // Common
        NotFound = 10,
        ValidationError = 11,
        Duplicated = 12,
        BadRequest=13,

        // Exam-specific
        ExamNotFound = 1000,
        InvalidData = 1001,

        // Instructor-specific
        NotValidInstructorBirthDate = 2000,
        InternalserverError = 400,
    }
}
