using Reclaimer.IO;

namespace Reclaimer.Blam.Halo5
{
    public class scenery
    {
        [Offset(160)]
        public TagReference hlmt { get; set; }

        public render_model GetModel() => hlmt.Tag?.ReadMetadata<model>().ReadRenderModel();
    }
}
