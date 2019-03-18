using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PixelStudio.Common
{
    public class VideoRenderer
    {
        private readonly SynchronizationContext _SyncContext;

        public VideoRenderer()
        {
            _SyncContext = SynchronizationContext.Current;
        }
        
        public void RenderVideo(ProjectModel project, VideoRendererSettings settings)
        {
            // TODO Async render, write each frame to a video stream and encode
        }

        // TODO Progress and complete events
    }
}
