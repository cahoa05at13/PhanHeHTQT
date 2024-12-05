using System;
using System.Collections.Generic;
using PhanHeHTQT.Models.DM;

namespace PhanHeHTQT.Models;

public partial class TbHinhThucDaoTaoCuaNganh
{
    public int IdHinhThucDaoTaoCuaNganh { get; set; }

    public int? IdNganhDaoTao { get; set; }

    public int? IdHinhThucDaoTao { get; set; }

    public int? SoNamDaoTao { get; set; }

    public virtual DmHinhThucDaoTao? IdHinhThucDaoTaoNavigation { get; set; }

    public virtual DmNganhDaoTao? IdNganhDaoTaoNavigation { get; set; }
}
