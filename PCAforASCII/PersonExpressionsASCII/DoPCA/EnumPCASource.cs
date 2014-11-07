namespace DoPCA
{
    /// <summary>
    /// 処理をしたデータソースが何であるかを指定する列挙型
    /// </summary>
    public enum PCASource
    {
        Bitmap,
        AsciiDataShapeOnly,
        AsciiDataTextureOnly,
        AsciiDataShapeAndTexture,
        other = 99
    }
}