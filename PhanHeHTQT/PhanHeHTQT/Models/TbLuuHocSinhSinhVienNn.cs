using System;
using System.Collections.Generic;
using PhanHeHTQT.Models.DM;

namespace PhanHeHTQT.Models;

public partial class TbLuuHocSinhSinhVienNn
{
    public int IdLuuHocSinhSinhVienNn { get; set; }

    public int? IdNguoiHoc { get; set; }

    public int? IdNguonKinhPhiLuuHocSinh { get; set; }

    public int? IdLoaiLuuHocSinh { get; set; }

    public virtual DmLoaiLuuHocSinh? IdLoaiLuuHocSinhNavigation { get; set; }

    public virtual DmNguonKinhPhiChoLuuHocSinh? IdNguonKinhPhiLuuHocSinhNavigation { get; set; }
}
