using System;

/// <summary>
/// Quản lý tập trung toàn bộ mã màu và thông số kích thước giao diện trong game.
/// Giúp dễ dàng tùy biến giao diện (Theming), sửa màu hoặc kích thước chỉ ở 1 file duy nhất.
/// </summary>
public static class UITheme
{
    #region Colors (Mã màu hệ int J2ME RGB)
    /// <summary> Màu xanh lá cây của tab/nút đang được chọn (Active/Focused) </summary>
    public const int COLOR_TAB_ACTIVE_GREEN = 6805896; // #67D848

    /// <summary> Màu vàng be của nền tab hoặc popup (Inactive/Normal) </summary>
    public const int COLOR_TAB_INACTIVE = 16773296; // #FFF0B0

    /// <summary> Màu xám nhạt của sub-tab không được chọn </summary>
    public const int COLOR_SUBTAB_INACTIVE = 14935011; // #E3E3E3

    /// <summary> Màu cam/nâu của đường viền, thanh kẻ phân cách </summary>
    public const int COLOR_ORANGE_LINE = 13524492; // #CE600C

    /// <summary> Màu nền vùng danh sách / ô item </summary>
    public const int COLOR_ITEM_BG = 15724264;

    /// <summary> Màu đen của viền khung ngoài </summary>
    public const int COLOR_BLACK = 0;

    /// <summary> Màu trắng </summary>
    public const int COLOR_WHITE = 16777215;
    #endregion

    #region Layout & Dimensions (Kích thước UI chuẩn)
    /// <summary> Chiều cao chuẩn của tab chính </summary>
    public const int MAIN_TAB_HEIGHT = 25;

    /// <summary> Chiều cao chuẩn của thanh sub-tab </summary>
    public const int SUB_TAB_HEIGHT = 20;

    /// <summary> Chiều cao của 1 ô/hàng vật phẩm trong danh sách </summary>
    public const int DEFAULT_ITEM_HEIGHT = 24;
    #endregion
}
