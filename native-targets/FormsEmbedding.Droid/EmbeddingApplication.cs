using Android.App;
using System;
using Android.Runtime;
namespace FormsEmbedding.Droid
{
    [Application]
    public class EmbeddingApplication : Application
    {
        public EmbeddingApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {

        }
    }
}