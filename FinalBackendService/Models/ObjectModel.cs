using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBackendService.Models
{
    public class ObjectModel
    {
        public string ObjectID { get; set; }
        public string ObjectInfoID { get; set; }
        public string DateSubmitted { get; set; }
		
    }
}
/*
 * 
 * 
		[ObjectID]			[NVARCHAR](255)						NOT NULL
	,	[ObjectInfoID]		[INT]								NOT NULL
	, 	[DateSubmitted]		[DATETIME]							NOT NULL
	, 	[DateAccepted]		[DATETIME]								NULL
	, 	[SubmitUser]		[NVARCHAR](100)						NOT NULL
	,	[AcceptUser]		[NVARCHAR](100)						 	NULL
	, 	[Active]			[BIT]								NOT NULL	DEFAULT 1
 * 
 */