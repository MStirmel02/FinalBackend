using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBackend.Services.Models
{
    public class FullObjectModel
    {
        public string ObjectID { get; set; }

		public string ObjectTypeID { get; set; }

		public string RightAscension { get; set; }

		public string Declination { get; set; }

		public double Redshift { get; set; }

		public double ApparentMagnitude { get; set; }

		public double AbsoluteMagnitude { get; set; }

		public string Mass { get; set; }

		public string Description { get; set; }

		public DateTime DateSubmitted { get; set; }

		public DateTime? DateAccepted {  get; set; }

		public string SubmitUser { get; set; }

		public string? AcceptUser { get; set; }

		public string Image {  get; set; }

    }
}

/*

	[ObjectTypeID]		
	,	[RightAscension]	
	,	[Declination]		
	,	[Redshift]			
	,	[ApparentMagnitude] 
	, 	[AbsoluteMagnitude]	
	,	[Mass]				
	, 	[Description]


	
		[ObjectID]			[NVARCHAR](255)						NOT NULL
	,	[ObjectInfoID]		[INT]								NOT NULL
	, 	[DateSubmitted]		[DATETIME]							NOT NULL
	, 	[DateAccepted]		[DATETIME]								NULL
	, 	[SubmitUser]		[NVARCHAR](100)						NOT NULL
	,	[AcceptUser]		[NVARCHAR](100)						 	NULL
	, 	[Image]				[NVARCHAR](255)						NOT NULL
	, 	[Active]			[BIT]								NOT NULL	DEFAULT 1

*/