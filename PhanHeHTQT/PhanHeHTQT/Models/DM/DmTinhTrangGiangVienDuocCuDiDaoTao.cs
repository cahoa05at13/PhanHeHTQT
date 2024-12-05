using System;
using System.Collections.Generic;

namespace PhanHeHTQT.Models.DM;

public partial class DmTinhTrangGiangVienDuocCuDiDaoTao
{
    public int IdTinhTrangGiangVienDuocCuDiDaoTao { get; set; }

    public string? TinhTrangGiangVienDuocCuDiDaoTao { get; set; }

    public virtual ICollection<TbGvduocCuDiDaoTao> TbGvduocCuDiDaoTaos { get; set; } = new List<TbGvduocCuDiDaoTao>();
}
