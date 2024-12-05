using System;
using System.Collections.Generic;
using PhanHeHTQT.Models.DM;

namespace PhanHeHTQT.Models;

public partial class TbDauMoiLienHe
{
    public int IdDauMoiLienHe { get; set; }

    public int? IdLoaiDauMoiLienHe { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public virtual DmDauMoiLienHe? IdLoaiDauMoiLienHeNavigation { get; set; }
}
