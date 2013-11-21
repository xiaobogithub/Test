using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;

namespace ChangeTech.ImageUploader
{
    public static class ClientInstance
    {
        public static IContainerContext ContaionerContext;
        public static Queue<string> FilesToUploadQueue = new Queue<string>();
    }
}
