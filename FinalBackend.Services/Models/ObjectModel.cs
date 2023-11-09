using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBackend.Services.Models
{
    public class ObjectModel
    {
        public string ObjectID { get; set; }
        public string ObjectInfoID { get; set; }
        public DateTime DateSubmitted { get; set; }
		public string UserSubmitted {  get; set; }
		public string Image {  get; set; }
		
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