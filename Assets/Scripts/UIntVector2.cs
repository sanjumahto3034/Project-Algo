public struct UIntVector2
{
    public int x;
    public int y;

    public UIntVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }


    /// <summary>
    /// Default value {0, 0}
    /// </summary>
    public static UIntVector2 Zero = new UIntVector2(0, 0);
}