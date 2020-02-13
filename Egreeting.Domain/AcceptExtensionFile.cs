using System.Collections.Generic;

namespace Egreeting.Domain
{
    public static class AcceptExtensionFile
    {
        public static readonly List<string> ListAcceptPicture = new List<string> { ".JPEG ", ".PNG", ".GIF" };
        public static readonly List<string> ListAcceptVideo = new List<string> { ".MP4 ", ".AVI", ".MOV", ".FLV" };
        public static readonly List<string> ListAcceptGIF = new List<string> { ".GIF" };
    }
}
