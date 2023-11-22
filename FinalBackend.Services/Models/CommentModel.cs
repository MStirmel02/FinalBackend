using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBackend.Services.Models
{
    public class CommentModel
    {
        /*
		[CommentID]			[INT]			IDENTITY(1,1)		NOT NULL
	,	[UserID]			[NVARCHAR](100)						NOT NULL
	,	[ObjectID]			[NVARCHAR](255)						NOT NULL
	,	[Description]		[TEXT]								NOT NULL
	,	[TimePosted]		[DATETIME]							NOT NULL
	,	[Active]			[BIT]								NOT NULL
		*/

		public int CommentID { get; set; }
		public string UserId { get; set; }
		public string ObjectId { get; set; }
		public string Description { get; set; }
		public DateTime TimePosted { get; set; }
		public bool Active { get; set; }

    }
}
