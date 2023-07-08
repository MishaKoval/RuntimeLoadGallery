using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Extensions
{
    public static class SpriteExtension
    {
        public static Sprite ConvertToSprite(this Texture2D texture)
        {
            texture.Compress(false);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
        
        public static Texture2D Scale(Texture sourceTex, int targetWidth, int targetHeight, TextureFormat format = TextureFormat.RGBA32)
        {
            if( sourceTex == null )
                throw new ArgumentException( "Parameter 'sourceTex' is null!" );

            Texture2D result = null;

            RenderTexture rt = RenderTexture.GetTemporary( targetWidth, targetHeight );
            RenderTexture activeRT = RenderTexture.active;

            try
            {
                Graphics.Blit( sourceTex, rt );
                RenderTexture.active = rt;

                result = new Texture2D( targetWidth, targetHeight, format,false);
                result.ReadPixels( new Rect( 0, 0, targetWidth, targetHeight ), 0, 0, false );
                result.Apply();
            }
            catch( Exception e )
            {
                Debug.LogException( e );

                Object.Destroy( result );
                result = null;
            }
            finally
            {
                RenderTexture.active = activeRT;
                RenderTexture.ReleaseTemporary( rt );
            }
            Object.Destroy(sourceTex);
            return result;
        }
    }
}