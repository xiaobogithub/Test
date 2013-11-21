using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class ProgramRoomModel
    {
        public Guid ProgramRoomGuid { get; set; }
        public ProgramModel Program { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TopBarColor { get; set; }
        public string ButtonNormal { get; set; }
        public string ButtonOver { get; set; }
        public string ButtonDown { get; set; }
        public string ButtonDisable { get; set; }
        public string CoverShadowColor { get; set; }
        public bool IsCoverShadowVisible { get; set; }
    }

    public class ProgramRoomsModel : List<ProgramRoomModel>
    { }
}
